using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code
{
    public static class Extensions
    {
        public static ExpandoObject Get(this ExpandoObject obj, string property)
        {
            if(obj.AsDictionary().TryGetValue(property, out var value))
            {
                return value;
            }
            return null;
        }

        public static T Get<T>(this ExpandoObject obj, string property)
        {
            if (obj.AsDictionary().TryGetValue(property, out var value))
            {
                return value;
            }
            throw new ArgumentException($"Object {obj} does not have property {property}");
        }

        public static IDictionary<string, dynamic> AsDictionary(this ExpandoObject obj)
        {
            return obj;
        }

        public static XElement TryGetElement(this XElement element, string childElementName)
        {
            return element.Elements().SingleOrDefault(x => x.Name.LocalName == childElementName);
        }

        public static XElement GetElement(this XElement element, string childElementName)
        {
            var child = element.TryGetElement(childElementName);
            return child ?? throw new ArgumentException(
                $"Missing child element '{childElementName}' in element '{element}'. " +
                $"Root element: {element.GetRootElement()}");
        }

        public static XElement GetRootElement(this XElement element)
        {
            return element.Parent == null ? element : GetRootElement(element.Parent);
        }

        public static string TryGetAttributeValue(this XElement element, string attribute)
        {
            return element.Attributes().SingleOrDefault(x => x.Name == attribute)?.Value;
        }

        public static string GetAttributeValue(this XElement element, string attribute)
        {
            var value = element.TryGetAttributeValue(attribute);
            return value ?? throw new ArgumentException($"Missing attribute '{attribute}' in element '{element}'");
        }
    }
}
