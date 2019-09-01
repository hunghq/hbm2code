using Hbm2Code.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code
{
    public class HbmParser
    {
        private static readonly IDictionary<string, ClassType> classTypes = new Dictionary<string, ClassType>()
        {
            {"class", ClassType.RootClass },
            {"subclass", ClassType.SubClass },
            {"joined-subclass", ClassType.JoinedSubClass }
        };

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

        private readonly XDocument hbmDocument;

        public HbmParser(XDocument hbmDocument)
        {
            this.hbmDocument = hbmDocument;
        }

        public static IPropertyParser GetPropertyParser(XElement element)
        {
            if (parsers.TryGetValue(element.Name.LocalName.ToLowerInvariant(), out var parser))
                return parser;
            throw new NotSupportedException($"No parser found for element <{element.Name.LocalName}>");
        }

        public IList<ClassInfo> Parse()
        {
            return hbmDocument.Root.Elements()
                .Where(x => classTypes.ContainsKey(x.Name.LocalName))
                .SelectMany(ParseClassInfo)
                .ToList();
        }

        private static ClassType ParseClassType(XElement clazzElement)
        {
            if (classTypes.TryGetValue(clazzElement.Name.LocalName, out var classType))
                return classType;
            throw new ArgumentException($"Unsupported class type: {clazzElement.Name.LocalName}");
        }

        private IEnumerable<ClassInfo> ParseClassInfo(XElement clazzElement)
        {
            var clazz = new ClassInfo
            {
                ClassType = ParseClassType(clazzElement),
                ClassName = clazzElement.GetAttributeValue("name"),
                Proxy = clazzElement.TryGetAttributeValue("proxy"),
                Extends = clazzElement.TryGetAttributeValue("extends"),
            };

            var clazzProp = CommonParser.ParseProperty(clazz, clazzElement);
            clazzProp.Remove("proxy");
            clazzProp.Remove("extends");

            clazz.OwnProperty.AddAttributes(clazzProp);
            clazz.OwnProperty.TagName = clazzElement.Name.LocalName;
            if (clazz.ClassType != ClassType.SubClass)
                clazz.OwnProperty.AddDefault("table", clazz.ClassName);

            foreach (var element in clazzElement.Elements())
            {
                if (classTypes.ContainsKey(element.Name.LocalName))
                {
                    yield return ParseClassInfo(element).FirstOrDefault();
                }
                else
                {
                    clazz.AddChildProperty(GetPropertyParser(element).Parse(clazz, element));
                }
            }

            yield return clazz;
        }
    }
}