using FluentAssertions;
using Hbm2Code.Tests.Utils;
using Xunit;

namespace Hbm2Code.Tests
{
    public class HbmCustomizerTest
    {
        [Fact]
        public void HbmCustomizer_ShouldChangePropertyAttribute()
        {
            ClassInfo clazz = TestUtils.ParseHbm("Agency.hbm.xml", "Agency", ClassType.JoinedSubClass);
            Property nameProp = clazz.GetAndAssertProperty("Name");

            var customizer = new HbmCustomizer();
            customizer.Register(LimitLengthOfName);
            customizer.Customize(clazz);

            nameProp.Should()
                .HaveName("OwnName")
                .HaveAttribute("length", "30")
                .HaveAttribute("column", "Name");
        }

        private void LimitLengthOfName(Property prop)
        {
            if (prop != null && prop.ClassInfo?.Extends == "BaseObject" && prop.Name == "Name")
            {
                prop.Name = "OwnName";
                prop["length"] = "30";
                prop.AddDefault("column", "Name");
            }
        }
    }
}