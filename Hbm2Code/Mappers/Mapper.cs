using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hbm2Code
{
    public static class Mapper
    {
        private static IDictionary<string, AttributeMapper> attributeMap = new Dictionary<string, AttributeMapper>()
        {
            {"not-null", new AttributeMapper("NotNullable")},
            {"table", new AttributeMapper(isStringValue: true)},
            {"column", new AttributeMapper(isStringValue: true)},
            {"name", new AttributeMapper(isStringValue: true)},
            {"index", new IndexAttributeMapper()},
            {"foreign-key", new AttributeMapper(isStringValue: true)},
            {"unique-key", new AttributeMapper(isStringValue: true)},
            {"formula", new AttributeMapper(isStringValue: true)},
            {"discriminator-value", new AttributeMapper(isStringValue: true)},
            {"composite-id", new AttributeMapper(methodName: "ComponentAsId")},
            {"key-many-to-one", new AttributeMapper(methodName: "ManyToOne")},
            {"key-property", new AttributeMapper(methodName: "Property")},
            {"type", new TypeAttributeMapper()},
            {"lazy", new LazyAttributeMapper()},
            {"fetch", new FetchAttributeMapper()},
            {"cascade", new CascadeAttributeMapper()},
            {"access", new AccessAttributeMapper()},
            {"class", new ClassAttributeMapper()},
            {"generator", new GeneratorAttributeMapper()},
            {"map-key-many-to-many", new AttributeMapper(methodName: "ManyToMany")},
            {"property-ref", new PropertyRefAttributeMapper() }
        };

        public static string MapTableName(ClassInfo clazz)
        {
            return clazz.TableName ?? clazz.ClassName;
        }

        public static string MapClassName(ClassInfo clazz)
        {
            return clazz.Proxy == null
                ? clazz.ClassName
                : Util.ToPascalCase(clazz.TableName);
        }

        public static string MapClassMapping(ClassInfo clazz)
        {
            switch (clazz.ClassType)
            {
                case ClassType.RootClass:
                    return (clazz.Proxy == null)
                        ? $"ClassMapping<{clazz.ClassName}>"
                        : $"ClassMapping<{ExtractProxyInterface(clazz)}>";
                case ClassType.JoinedSubClass:
                    return $"JoinedSubclassMapping<{clazz.ClassName}>";
                case ClassType.SubClass:
                    return $"SubclassMapping<{clazz.ClassName}>";
                default:
                    throw new ArgumentException("Invalid class type: " + clazz.ClassType);
            }
        }

        private static string ExtractProxyInterface(ClassInfo clazz)
        {
            var regex = new Regex(@"(\w+),");
            var match = regex.Match(clazz.Proxy);
            if (!match.Success)
                throw new ArgumentException($"Class has invalid proxy interface: {clazz.Proxy}");

            return match.Groups[1].Value;
        }

        public static string MapAttributeMethod(Property prop, string attributeName)
        {
            if (attributeMap.TryGetValue(attributeName, out var attributeMapper)
                && attributeMapper.MethodNameMapper != null)
            {
                return attributeMapper.MethodNameMapper(prop);
            }

            return Util.ToPascalCase(attributeName);
        }

        public static string MapAttributeValue(Property property, string attributeName)
        {
            if (!property.ContainsKey(attributeName))
                throw new ArgumentException($"Property {property} do not have attribute {attributeName}");

            var attributeValue = property[attributeName];
            if (attributeMap.TryGetValue(attributeName, out var attributeMapper))
            {
                string value = attributeValue;
                if (attributeMapper.ValueMapper != null)
                    value = attributeMapper.ValueMapper.Invoke(property, attributeValue);

                return attributeMapper.IsStringValue ? string.Format("\"{0}\"", value) : value;
            }

            return attributeValue;
        }
    }
}
