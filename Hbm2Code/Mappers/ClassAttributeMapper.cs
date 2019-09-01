namespace Hbm2Code
{
    public class ClassAttributeMapper : AttributeMapper
    {
        public ClassAttributeMapper() : base(valueMapper: MapClass)
        {
        }

        public static string MapClass(Property property, string className)
        {
            return $"typeof({className})";
        }
    }
}