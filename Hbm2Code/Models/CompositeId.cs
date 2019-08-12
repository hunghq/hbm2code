using System.Collections.Generic;

namespace Hbm2Code
{
    public class CompositeId : Property
    {
        public CompositeId(IClassInfo classInfo, string name) : base(classInfo, name)
        {
            TagName = "composite-id";
            ManyToOneKeys = new List<Property>();
            PropertyKeys = new List<Property>();
        }

        public IList<Property> ManyToOneKeys { get; }

        public IList<Property> PropertyKeys { get; }

        public override IReadOnlyList<Property> ChildProperties
        {
            get
            {
                var list = new List<Property>(ManyToOneKeys);
                list.AddRange(PropertyKeys);
                return list;
            }
        }
    }
}