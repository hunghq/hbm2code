using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class CompositeIdParser : BasicPropertyParser
    {
        public CompositeIdParser() : base("composite-id")
        {
        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            var prop = base.Parse(clazz, element);
            var compositeId = new CompositeId(clazz, prop.Name);
            compositeId.AddAttributes(prop);
            compositeId.Remove("class");

            element.Elements().Where(x => x.Name.LocalName == "key-many-to-one").ToList()
                .ForEach(key => compositeId.ManyToOneKeys.Add(base.Parse(clazz, key)));

            element.Elements().Where(x => x.Name.LocalName == "key-property").ToList()
                .ForEach(key => compositeId.PropertyKeys.Add(ParseKeyProperty(clazz, key)));

            return compositeId;
        }

        private Property ParseKeyProperty(ClassInfo clazz, XElement property)
        {
            var keyProperty = base.Parse(clazz, property);
            var columnElement = property.TryGetElement("column");
            if (columnElement != null)
            {
                var column = base.Parse(clazz, columnElement);
                keyProperty.AddDefault("column", column.Name);
                keyProperty.AddAttributes(column);
            }

            return keyProperty;
        }
    }
}
