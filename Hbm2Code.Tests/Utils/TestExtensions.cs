using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Hbm2Code.Tests.Utils
{
    public static class TestExtensions
    {
        public static ClassInfo AssertHasClass(this IList<ClassInfo> clazzInfos, string className, ClassType classType)
        {
            var clazz = clazzInfos.FirstOrDefault(x => x.ClassName == className && x.ClassType == classType);
            clazz.Should().NotBeNull($"There should be a class with name = {className}, type = {classType}");
            return clazz;
        }

        public static Property GetAndAssertProperty(this ClassInfo clazz, string propertyName)
        {
            return clazz.GetProperties().GetAndAssertProperty(propertyName, clazz.ClassName);
        }

        public static Property GetAndAssertProperty(this IList<Property> properties, string propertyName, string className)
        {
            List<Property> found = properties.Where(x => x.Name == propertyName).ToList();
            found.Should().HaveCount(1, $"Missing property '{propertyName}' in class {className}");
            return found.Single();
        }
    }
}