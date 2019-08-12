using System.Collections.Generic;

namespace Hbm2Code
{
    public class Map : Property
    {
        public Map(IClassInfo classInfo, string name) : base(classInfo, name)
        {
            TagName = "map";
        }

        public Property MapTypeProperty { get; set; }
        public Property RelationProperty { get; set; }
        public Property KeyProperty { get; set; }

        public override IReadOnlyList<Property> ChildProperties
        {
            get
            {
                return KeyProperty == null ? new List<Property>() : new List<Property>() { KeyProperty };
            }
        }

        public override IReadOnlyList<IReadOnlyList<Property>> ExtendedPropertySets
        {
            get
            {
                return new List<List<Property>>()
                {
                    new List<Property>() { MapTypeProperty },
                    new List<Property>() { RelationProperty }
                };
            }
        }
    }
}
