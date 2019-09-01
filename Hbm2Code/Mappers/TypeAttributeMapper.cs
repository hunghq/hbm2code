using System.Collections.Generic;
using System.Linq;

namespace Hbm2Code
{
    public class TypeAttributeMapper : AttributeMapper
    {
        public TypeAttributeMapper() : base(valueMapper: MapType)
        {
        }

        private static IDictionary<string, string> TypeMapper = new Dictionary<string, string>()
            {
                { "Int32", "NHibernateUtil.Int32"},
                { "int", "NHibernateUtil.Int32"},
                { "Int64", "NHibernateUtil.Int64"},
                { "String", "NHibernateUtil.String"},
                { "DateTime", "NHibernateUtil.DateTime"},
                { "Guid", "NHibernateUtil.Guid"},
                { "Boolean", "NHibernateUtil.Boolean"},
                { "AnsiString", "NHibernateUtil.AnsiString"},
                { "BinaryBlob", "NHibernateUtil.BinaryBlob"}
            };

        public static string MapType(Property property, string type)
        {
            if (property is CompositeUserProperty)
                return $"typeof({type}), null";

            var foundKey = TypeMapper.Keys.FirstOrDefault(k =>
                string.Equals(k, type, System.StringComparison.InvariantCultureIgnoreCase));

            return foundKey != null
                ? TypeMapper[foundKey]
                : $"typeof(IntEnumType<{type}>), null";
        }
    }
}