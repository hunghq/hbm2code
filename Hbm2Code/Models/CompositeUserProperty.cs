using System.Collections.Generic;

namespace Hbm2Code
{
    public class CompositeUserProperty : Property
    {
        public ColumnsProperty ColumnsProperty { get; }

        public CompositeUserProperty(IClassInfo classInfo, string name) : base(classInfo, name)
        {
            ColumnsProperty = new ColumnsProperty(classInfo);
        }

        public override IReadOnlyList<Property> ChildProperties => new List<Property>() { ColumnsProperty };
    }

    public class ColumnsProperty : Property
    {
        public ColumnsProperty(IClassInfo classInfo) : base(classInfo, "")
        {
            TagName = "columns";
            IgnoreOwnAttributes = true;
        }

        private List<List<Property>> _extendedPropertySets = new List<List<Property>>();

        public override IReadOnlyList<Property> ChildProperties => base.ChildProperties;

        public override IReadOnlyList<IReadOnlyList<Property>> ExtendedPropertySets => _extendedPropertySets;

        public void AddColumn(Property column)
        {
            column.IsEmbeded = true;
            if (!string.IsNullOrEmpty(column.Name))
                column.Add("name", column.Name);

            _extendedPropertySets.Add(new List<Property>() { column });
        }
    }
}