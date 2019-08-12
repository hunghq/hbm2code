using System;

namespace Hbm2Code
{
    public class CascadeAttributeMapper : AttributeMapper
    {
        public CascadeAttributeMapper() : base(valueMapper: MapCascade)
        {
        }

        public static string MapCascade(Property property, string cascade)
        {
            switch (cascade)
            {
                case "none":
                    return "Cascade.None";
                case "save-update":
                    return "Cascade.Persist";
                case "delete-orphans":
                    return "Cascade.DeleteOrphans";
                case "all-delete-orphan":
                    return "Cascade.All | Cascade.DeleteOrphans";
                case "all":
                    return "Cascade.All";
                default:
                    throw new ArgumentException("Invalid cascade: " + cascade);
            }
        }
    }
}
