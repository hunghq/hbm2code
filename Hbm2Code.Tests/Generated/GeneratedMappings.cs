using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Hbm2Code.Tests.DomainModels;

namespace Hbm2Code.Tests.Generated
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
            
            Set(x => x.Agencies, m =>
            {
                m.Access(Accessor.Property);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
                m.Cascade(Cascade.All);
                m.BatchSize(20);
                m.Key(n =>
                {
                    n.Column("AreaId");
                });
                
            }, m =>
            {
                m.OneToMany(n =>
                {
                    n.Class(typeof(Agency));
                });
                
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

    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("Category");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Assigned);
                m.Type(NHibernateUtil.Int64);
            });
            
            Property(x => x.Name, m =>
            {
            });
            
            Bag(x => x.Items, m =>
            {
                m.Table("CategoryItem");
                m.Key(n =>
                {
                    n.Column("CategoryId");
                });
                
            }, m =>
            {
                m.ManyToMany(n =>
                {
                    n.Class(typeof(Item));
                    n.Column("ItemId");
                });
                
            });
            
        }
    }

    public class ItemMap : ClassMapping<Item>
    {
        public ItemMap()
        {
            Table("Item");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Assigned);
                m.Type(NHibernateUtil.Int64);
            });
            
            Property(x => x.Name, m =>
            {
            });
            
            List(x => x.Categories, m =>
            {
                m.Table("CategoryItem");
                m.Inverse(true);
                m.Key(n =>
                {
                    n.Column("ItemId");
                });
                
            }, m =>
            {
                m.ManyToMany(n =>
                {
                    n.Class(typeof(Category));
                    n.Column("CategoryId");
                });
                
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
