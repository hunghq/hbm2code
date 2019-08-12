using Hbm2Code.DomainModels;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hbm2Code.Mapping
{
    public class BaseObjectMap : ClassMapping<BaseObject>
    {
        public BaseObjectMap()
        {
            Table("BaseObject");
            Abstract(true);

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Assigned);
                m.Type(NHibernateUtil.Int64);
            });
            
            Version(x => x.Version, m =>
            {
                m.Column("Version");
                m.Type(NHibernateUtil.Int64);
                m.UnsavedValue(0);
            });
            
            Property(x => x.Name, m =>
            {
                m.Type(NHibernateUtil.String);
                m.NotNullable(false);
                m.Length(255);
                m.Index("IX_EntityName");
            });
            
        }
    }

}
