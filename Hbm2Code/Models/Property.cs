using System.Collections.Generic;
using System.Linq;

namespace Hbm2Code
{
    public class Property : Dictionary<string, string>
    {
        public string Name { get; set; }

        public string TagName { get; set; }

        public IClassInfo ClassInfo { get; }

        public bool IsEmbeded { get; set; }

        public bool IgnoreOwnAttributes { get; set; }

        public Property(IClassInfo classInfo, string name)
        {
            ClassInfo = classInfo;
            Name = name;
        }

        public virtual IReadOnlyList<Property> ChildProperties => Enumerable.Empty<Property>().ToList();

        public virtual IReadOnlyList<IReadOnlyList<Property>> ExtendedPropertySets => new List<List<Property>>();
        
        public void AddAttributes(IDictionary<string, string> attributes)
        {
            foreach (var attribute in attributes)
            {
                Add(attribute.Key, attribute.Value);
            }
        }

        public void AddDefault(string attributeName, string attributeValue)
        {
            if (!ContainsKey(attributeName))
                Add(attributeName, attributeValue);
        }

        public override string ToString()
        {
            return $"<{TagName} name=\"{Name}\" />";
        }
    }
}