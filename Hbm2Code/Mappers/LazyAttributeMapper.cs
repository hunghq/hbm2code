using System;

namespace Hbm2Code
{
    public class LazyAttributeMapper : AttributeMapper
    {
        public LazyAttributeMapper() : base(valueMapper: MapLazy)
        {

        }

        private static string MapLazy(Property property, string lazyType)
        {
            switch (property.TagName)
            {
                case "property":
                    return lazyType;

                case "set":
                case "map":
                    switch (lazyType)
                    {
                        case "true":
                            return "CollectionLazy.Lazy";
                        case "false":
                            return "CollectionLazy.NoLazy";
                        default:
                            throw new ArgumentException($"Unsupported lazy type: {lazyType} in <{property.TagName}>");
                    }

                case "many-to-one":
                case "one-to-one":
                case "key-many-to-one":
                    switch (lazyType)
                    {
                        case "proxy":
                            return "LazyRelation.Proxy";
                        case "false":
                            return "LazyRelation.NoLazy";
                        default:
                            throw new ArgumentException($"Unsupported lazy type: {lazyType} in <{property.TagName}>");
                    }
                case "joined-subclass":
                case "class":
                    return lazyType;

                default:
                    throw new ArgumentException($"Unsupported lazy type: {lazyType} in <{property.TagName}>");
            }
        }
    }
}
