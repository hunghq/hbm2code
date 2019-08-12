using System.Collections.Generic;
using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public class ComponentParser : BasicPropertyParser
    {
        public ComponentParser() : base("component")
        {

        }

        public override Property Parse(ClassInfo clazz, XElement element)
        {
            var property = base.Parse(clazz, element);
            var component = new Component(clazz, property.Name);
            component.AddAttributes(property);

            foreach (var childElement in element.Elements())
                component.AddChildProperty(HbmParser.GetPropertyParser(childElement).Parse(clazz, childElement));

            return component;
        }
    }

    public class Component : Property
    {
        public Component(IClassInfo classInfo, string name) : base(classInfo, name)
        {
            TagName = "component";
        }

        private readonly List<Property> childProperties = new List<Property>();

        public void AddChildProperty(Property property)
        {
            childProperties.Add(property);
        }

        public override IReadOnlyList<Property> ChildProperties => childProperties;
    }
}