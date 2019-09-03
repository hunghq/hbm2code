using Hbm2Code.DomainModels;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hbm2Code.Mapping
{
    public class AgencyMap : JoinedSubclassMapping<Agency>
    {
        public AgencyMap()
        {
            Table("Agency");

            Key(m =>
            {
                m.Column("Id");
                m.ForeignKey("FK_Application");
            });
            
            Property(x => x.Name, m =>
            {
                m.Type(NHibernateUtil.String);
                m.NotNullable(true);
                m.Length(255);
                m.Unique(true);
            });
            
            Property(x => x.Guid, m =>
            {
                m.Type(NHibernateUtil.Guid);
            });
            
            ManyToOne(x => x.Area, m =>
            {
                m.Column("AreaId");
                m.Lazy(LazyRelation.Proxy);
                m.NotNullable(true);
                m.ForeignKey("FK_Agency_Area");
            });
            
        }
    }

    public class HeadQuarterMap : ClassMapping<HeadQuarter>
    {
        public HeadQuarterMap()
        {
            Table("HeadQuarter");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Foreign<HeadQuarter>(y => y.Agency));
                m.Type(NHibernateUtil.Int64);
            });
            
            OneToOne(x => x.Agency, m =>
            {
                m.Constrained(true);
                m.ForeignKey("FK_Agency_HeadQuarter");
            });
            
        }
    }

    public class AreaMap : ClassMapping<Area>
    {
        public AreaMap()
        {
            Table("Area");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Assigned);
            });
            
            Property(x => x.Name, m =>
            {
            });
            
        }
    }

    public class BaseObjectMap : ClassMapping<BaseObject>
    {
        public BaseObjectMap()
        {
            Abstract(true);
            Table("BaseObject");

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
            
        }
    }

    public class DomesticWorkerMap : SubclassMapping<DomesticWorker>
    {
        public DomesticWorkerMap()
        {
            DiscriminatorValue("DW");

            Property(x => x.SocialSecurityNo, m =>
            {
            });
            
        }
    }

    public class WorkerMap : ClassMapping<Worker>
    {
        public WorkerMap()
        {
            Abstract(true);
            Table("Worker");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Assigned);
                m.Type(NHibernateUtil.Int64);
            });
            
            Discriminator(m =>
            {
                m.Column("Type");
            });
            
            Property(x => x.Name, m =>
            {
            });
            
        }
    }

    public class ForeignWorkerMap : SubclassMapping<ForeignWorker>
    {
        public ForeignWorkerMap()
        {
            DiscriminatorValue("FW");

            Property(x => x.PassportNo, m =>
            {
            });
            
        }
    }

}
