using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class MapParser : BasicPropertyParser
    {
        public MapParser() : base("map")
        {
        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            var prop = base.Parse(clazz, element);
            var map = new Map(clazz, prop.Name);
            map.AddAttributes(prop);

            XElement key = element.TryGetElement("key");
            if (key != null)
                map.KeyProperty = base.Parse(clazz, key);

            XElement index = element.TryGetElement("index");
            if (index != null)
                map.MapTypeProperty = base.Parse(clazz, index);

            XElement manyToMany = element.TryGetElement("map-key-many-to-many");
            if (manyToMany != null)
            {
                manyToMany.Attribute("class").Remove();
                map.MapTypeProperty = base.Parse(clazz, manyToMany);
            }

            XElement oneToMany = element.TryGetElement("one-to-many");
            if (oneToMany != null)
                map.RelationProperty = base.Parse(clazz, oneToMany);
            return map;
        }
    }
}
