namespace Hbm2Code
{
    public class IndexAttributeMapper : AttributeMapper
    {
        public IndexAttributeMapper() : base(methodNameMapper: IndexMethodNameMapper, isStringValue: true)
        {
        }

        private static string IndexMethodNameMapper(Property property)
        {
            return property.TagName == "index" ? "Element" : "Index";
        }
    }
}