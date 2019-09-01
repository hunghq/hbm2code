using System;
using System.Collections.Generic;
using System.Linq;

namespace Hbm2Code
{
    public class ClassInfo : IClassInfo
    {
        public string ClassName { get; set; }
        public ClassType ClassType { get; set; }
        public string TableName { get; set; }

        public string Proxy { get; set; }
        public string Extends { get; set; }

        public ClassInfo()
        {
            OwnProperty = new Property(this, null);
        }

        public Property OwnProperty { get; }

        private IList<Property> childProperties = new List<Property>();

        public void AddChildProperty(Property property)
        {
            if (property == null)
                throw new ArgumentException("Cannot add null property to class info");
            childProperties.Add(property);
        }

        public T GetChildProperty<T>(string tagName) where T : Property
        {
            return childProperties.SingleOrDefault(x =>
                string.Equals(x.TagName, tagName, StringComparison.InvariantCultureIgnoreCase)) as T;
        }

        public IList<T> GetChildProperies<T>(string tagName) where T : Property
        {
            return childProperties.OfType<T>()
                .Where(x => string.Equals(x.TagName, tagName, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        public IList<Property> GetChildProperties() => childProperties;
    }
}