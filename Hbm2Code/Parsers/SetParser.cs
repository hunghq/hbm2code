using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class SetParser : BasicPropertyParser
    {
        public SetParser() : base("set")
        {
        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            var prop = base.Parse(clazz, element);
            var set = new Set(clazz, prop.Name);
            set.AddAttributes(prop);

            set.KeyProperty = base.Parse(clazz, element.GetElement("key"));
            set.RelationProperty = base.Parse(clazz,
                element.TryGetElement("one-to-many") ?? element.GetElement("many-to-many"));
            return set;
        }
    }
}