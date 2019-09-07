using System.Collections.Generic;

namespace Hbm2Code
{
    public static class ClassInfoExtensions
    {
        public static IdProperty GetIdProperty(this ClassInfo clazz) => clazz.GetChildProperty<IdProperty>("id");

        public static Property GetDiscriminator(this ClassInfo clazz) => clazz.GetChildProperty<Property>("discriminator");

        public static Property GetKeyProperty(this ClassInfo clazz) => clazz.GetChildProperty<Property>("key");

        public static CompositeId GetCompositeId(this ClassInfo clazz) => clazz.GetChildProperty<CompositeId>("composite-id");

        public static IList<Property> GetProperties(this ClassInfo clazz) => clazz.GetChildProperties<Property>("property");

        public static IList<Property> GetManyToOneProperties(this ClassInfo clazz) => clazz.GetChildProperties<Property>("many-to-one");

        public static IList<Property> GetOneToOneProperties(this ClassInfo clazz) => clazz.GetChildProperties<Property>("one-to-one");

        public static IList<Collection> GetSets(this ClassInfo clazz) => clazz.GetChildProperties<Collection>("set");

        public static IList<Collection> GetBags(this ClassInfo clazz) => clazz.GetChildProperties<Collection>("bag");
    }
}