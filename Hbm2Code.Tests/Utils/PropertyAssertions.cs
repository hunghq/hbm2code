using FluentAssertions;
using FluentAssertions.Collections;

namespace Hbm2Code.Tests.Utils
{
    public class PropertyAssertions : GenericDictionaryAssertions<string, string>
    {
        private readonly Property property;

        public PropertyAssertions(Property property) : base(property)
        {
            this.property = property;
            Subject = property;
        }

        protected override string Identifier => "property";

        [CustomAssertion]
        public PropertyAssertions HaveTagName(string expectedName)
        {
            property.TagName.Should().Be(expectedName,
                $"Property {property} should have tag name '{expectedName}'");
            return this;
        }

        [CustomAssertion]
        public PropertyAssertions HaveName(string expectedName)
        {
            property.Name.Should().Be(expectedName,
                $"Property {property} should have name '{expectedName}'");
            return this;
        }

        [CustomAssertion]
        public PropertyAssertions HaveAttribute(string attributeName, string attributeValue)
        {
            property.Should().ContainKey(attributeName,
                $"Property '{property}' should have attribute '{attributeName}'.");

            property[attributeName].Should().Be(attributeValue,
                $"Property '{property}' should have attribute '{attributeName}' with value '{attributeValue}'");

            return this;
        }

        [CustomAssertion]
        public PropertyAssertions NotHaveAttribute(string attributeName)
        {
            property.Should().NotContainKey(attributeName,
                $"Property '{property}' should not have attribute '{attributeName}'.");

            return this;
        }
    }
}