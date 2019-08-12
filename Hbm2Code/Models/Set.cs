using System.Collections.Generic;

namespace Hbm2Code
{
    public class Set : Property
    {
        public Property KeyProperty { get; set; }
        public Property RelationProperty { get; set; }

        public Set(IClassInfo classInfo, string name) : base(classInfo, name)
        {
            TagName = "set";
        }

        public override IReadOnlyList<Property> ChildProperties => new List<Property>() { KeyProperty };

        public override IReadOnlyList<IReadOnlyList<Property>> ExtendedPropertySets
        {
            get
            {
                return new List<List<Property>>()
                {
                    new List<Property>() { RelationProperty }
                };
            }
        }
    }
}