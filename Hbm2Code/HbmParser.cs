using Hbm2Code.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code
{
    public class HbmParser
    {
        private static readonly IDictionary<string, IPropertyParser> parsers = new List<IPropertyParser>()
        {
            new PropertyParser(),
            new BasicPropertyParser("key"),
            new BasicPropertyParser("version"),
            new BasicPropertyParser("discriminator"),
            new BasicPropertyParser("many-to-one"),
            new BasicPropertyParser("one-to-one"),
            new ComponentParser(),
            new CompositeIdParser(),
            new IdParser(),
            new SetParser(),
            new MapParser()
        }.ToDictionary(p => p.TagName.ToLowerInvariant());

        private static readonly IDictionary<string, ClassType> classTypes = new Dictionary<string, ClassType>()
        {
            {"class", ClassType.RootClass },
            {"subclass", ClassType.SubClass },
            {"joined-subclass", ClassType.JoinedSubClass }
        };

        private readonly XDocument hbmDocument;

        public HbmParser(XDocument hbmDocument)
        {
            this.hbmDocument = hbmDocument;
        }

        public IList<ClassInfo> Parse()
        {
            return hbmDocument.Root.Elements()
                .Where(x => classTypes.ContainsKey(x.Name.LocalName))
                .Select(ParseClassInfo)
                .ToList();
        }

        private ClassInfo ParseClassInfo(XElement clazzElement)
        {
            var clazz = new ClassInfo
            {
                ClassType = ParseClassType(clazzElement),
                ClassName = clazzElement.GetAttributeValue("name"),
                TableName = clazzElement.TryGetAttributeValue("table"),
                Proxy = clazzElement.TryGetAttributeValue("proxy"),
                Extends = clazzElement.TryGetAttributeValue("extends"),
                DiscriminatorValue = clazzElement.TryGetAttributeValue("discriminator-value"),
                Abstract = clazzElement.TryGetAttributeValue("abstract")
            };

            var clazzProp = CommonParser.ParseProperty(clazz, clazzElement);
            clazzProp.Remove("table");
            clazzProp.Remove("proxy");
            clazzProp.Remove("extends");
            clazz.OwnProperty.AddAttributes(clazzProp);
            clazz.OwnProperty.TagName = clazzElement.Name.LocalName;

            foreach (var element in clazzElement.Elements())
                clazz.AddChildProperty(GetPropertyParser(element).Parse(clazz, element));

            return clazz;
        }

        public static IPropertyParser GetPropertyParser(XElement element)
        {
            if (parsers.TryGetValue(element.Name.LocalName.ToLowerInvariant(), out var parser))
                return parser;
            throw new NotSupportedException($"No parser found for element <{element.Name.LocalName}>");
        }

        private static ClassType ParseClassType(XElement clazzElement)
        {
            if (classTypes.TryGetValue(clazzElement.Name.LocalName, out var classType))
                return classType;
            throw new ArgumentException($"Unsupported class type: {clazzElement.Name.LocalName}");
        }
    }
}
