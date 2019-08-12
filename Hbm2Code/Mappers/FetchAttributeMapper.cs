using System;

namespace Hbm2Code
{
    public class FetchAttributeMapper : AttributeMapper
    {
        public FetchAttributeMapper() : base(valueMapper: MapFetchKind)
        {
        }

        private static string MapFetchKind(Property property, string fetchKind)
        {
            switch (property.TagName)
            {
                case "many-to-one":
                    switch (fetchKind)
                    {
                        case "select":
                            return "FetchKind.Select";
                        case "join":
                            return "FetchKind.Join";
                        default:
                            throw new ArgumentException($"Invalid fetch kind: {fetchKind} in <{property.TagName}>");
                    }

                case "set":
                    switch (fetchKind)
                    {
                        case "select":
                            return "CollectionFetchMode.Select";
                        case "join":
                            return "CollectionFetchMode.Join";
                        default:
                            throw new ArgumentException($"Invalid fetch kind: {fetchKind} in <{property.TagName}>");
                    }
                default:
                    throw new ArgumentException($"Invalid fetch kind: {fetchKind} in <{property.TagName}>");
            }
        }
    }
}
