using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class IdParser : BasicPropertyParser
    {
        public IdParser() : base("id")
        {
        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            var prop = base.Parse(clazz, element);
            var generatorElement = element.GetElement("generator");
            var generatorClass = generatorElement.GetAttributeValue("class");

            var idProp = new IdProperty(clazz, prop.Name, generatorClass);
            idProp.AddAttributes(prop);

            generatorElement.Elements().Where(x => x.Name.LocalName == "param").ToList()
                .ForEach(p => idProp.GeneratorParams.Add(p.GetAttributeValue("name"), p.Value));

            return idProp;
        }
    }
}