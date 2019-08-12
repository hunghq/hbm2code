namespace Hbm2Code
{
    public class PropertyRefAttributeMapper : AttributeMapper
    {
        public PropertyRefAttributeMapper() : base("PropertyReference", valueMapper: MapPropertyRef)
        {
        }

        public static string MapPropertyRef(Property property, string value)
        {
            return $"y => y.{value}";
        }
    }
}
