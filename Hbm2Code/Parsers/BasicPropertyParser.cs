using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class BasicPropertyParser : IPropertyParser
    {
        public string TagName { get; }

        public BasicPropertyParser(string tagName)
        {
            TagName = tagName;
        }

        public virtual Property Parse(ClassInfo clazz, XElement element)
        {
            return CommonParser.ParseProperty(clazz, element);
        }
    }
}