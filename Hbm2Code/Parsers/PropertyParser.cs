using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class PropertyParser : BasicPropertyParser
    {
        public PropertyParser() : base("property")
        {
        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            List<XElement> columns = element.Elements().Where(x => x.Name.LocalName == "column").ToList();
            return columns.Count == 0
                ? ParseNormalProperty(clazz, element)
                : ParseCompositeUserProperty(clazz, element, columns);
        }

        private Property ParseNormalProperty(ClassInfo clazz, XElement element)
        {
            return base.Parse(clazz, element);
        }

        private Property ParseCompositeUserProperty(ClassInfo clazz, XElement element, List<XElement> columns)
        {
            var prop = base.Parse(clazz, element);
            var compositeProp = new CompositeUserProperty(clazz, prop.Name)
            {
                TagName = "property"
            };
            compositeProp.AddAttributes(prop);

            foreach (var column in columns)
            {
                var columnProp = base.Parse(clazz, column);
                compositeProp.ColumnsProperty.AddColumn(columnProp);
            }

            return compositeProp;
        }
    }
}