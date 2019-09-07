using System.Collections.Generic;

namespace Hbm2Code
{
    public class Collection : Property
    {
        public Property KeyProperty { get; set; }
        public Property RelationProperty { get; set; }

        public Collection(IClassInfo classInfo, string name, string tagName) : base(classInfo, name)
        {
            TagName = tagName;
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