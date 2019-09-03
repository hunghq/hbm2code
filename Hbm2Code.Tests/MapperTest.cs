using FluentAssertions;
using Xunit;

namespace Hbm2Code.Tests
{
    public class MapperTest
    {
        [Fact]
        public void MapAttribute_StringValue_ShouldHaveDoubleQuotes()
        {
            var prop = new Property(null, "test")
            {
                { "index", "IX_TEST" }
            };

            Mapper.MapAttributeValue(prop, "index").Should().Be(@"""IX_TEST""");
        }

        [Fact]
        public void MapAttribute_NameHavingDash_ShouldBecomePascalCase()
        {
            Mapper.MapAttributeMethod(null, "foreign-key").Should().Be("ForeignKey");
        }

        [Fact]
        public void MapAttribute_IndexOfMap_ShouldBecomeElement()
        {
            var indexProp = new Property(null, "")
            {
                TagName = "index"
            };

            Mapper.MapAttributeMethod(indexProp, "index").Should().Be("Element");
        }

        [Fact]
        public void MapAttribute_ManyToManyOfMap_ShouldBecomeManyToMany()
        {
            var indexProp = new Property(null, "")
            {
                TagName = "map-key-many-to-many"
            };

            Mapper.MapAttributeMethod(indexProp, "map-key-many-to-many").Should().Be("ManyToMany");
        }
    }
}