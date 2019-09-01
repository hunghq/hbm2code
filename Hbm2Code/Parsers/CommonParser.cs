using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public static class CommonParser
    {
        public static Property ParseProperty(ClassInfo clazz, XElement element)
        {
            var prop = new Property(clazz, element.TryGetAttributeValue("name"))
            {
                TagName = element.Name.LocalName
            };

            prop.AddAttributes(element.Attributes()
                .Where(x => x.Name.LocalName != "name")
                .ToDictionary(x => x.Name.LocalName, x => x.Value));
            return prop;
        }
    }
}