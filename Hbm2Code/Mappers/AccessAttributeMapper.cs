using NHibernate.Mapping.ByCode;
using System;
using System.Text.RegularExpressions;

namespace Hbm2Code
{
    public class AccessAttributeMapper : AttributeMapper
    {
        public AccessAttributeMapper() : base(valueMapper: MapAccess)
        {
        }

        public static string MapAccess(Property property, string access)
        {
            switch (property.TagName.ToLowerInvariant())
            {
                case "id":
                    return MapIdAccess(property, access);
                default:
                    return MapFieldAccess(access);
            }
        }

        private static string MapFieldAccess(string access)
        {
            switch (access)
            {
                case "field.camelcase-underscore":
                    return "Accessor.Field";
                case "property":
                    return "Accessor.Property";
                case "readonly":
                    return "Accessor.ReadOnly";
                default:
                    throw new ArgumentException("Invalid access value: " + access);
            }
        }

        public static string MapIdAccess(Property property, string access)
        {
            var idProperty = property as IdProperty;
            if (idProperty == null)
                throw new ArgumentException($"Property {property.Name} in class " +
                    $"{property.ClassInfo.ClassName} must be a IdProperty to map access attribute");

            if (idProperty.ClassInfo.Proxy == null)
                throw new ArgumentException($"Class {idProperty.ClassInfo.ClassName} should have proxy interface to map Id access");

            ExtractIdAccess(access, out string accessorType, out string proxyType);
            return $"typeof({accessorType}<{proxyType}>)";
        }

        private static void ExtractIdAccess(string access, out string accessorType, out string proxyType)
        {
            var regex = new Regex(@"(\w+)`1{1}.+\.(\w+),");
            var match = regex.Match(access);
            if (!match.Success)
                throw new ArgumentException($"Class has Id access attribute: {access}");
            accessorType = match.Groups[1].Value;
            proxyType = match.Groups[2].Value;
        }
    }
}
