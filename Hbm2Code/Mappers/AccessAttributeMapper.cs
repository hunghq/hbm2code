using System;

namespace Hbm2Code
{
    public class AccessAttributeMapper : AttributeMapper
    {
        public AccessAttributeMapper() : base(valueMapper: MapAccess)
        {
        }

        public static string MapAccess(Property property, string access)
        {
            switch (access)
            {
                case "field.camelcase-underscore":
                    return "Accessor.Field";

                case "property":
                    return "Accessor.Property";

                case "readonly":
                    return "Accessor.ReadOnly";

                default:
                    throw new ArgumentException("Invalid access value: " + access);
            }
        }
    }
}