# hbm2code

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)  [![forthebadge](https://forthebadge.com/images/badges/kinda-sfw.svg)](https://forthebadge.com)

Convert NHibernate XML mapping to Code mapping

[![Build status](https://ci.appveyor.com/api/projects/status/eo1djphwh9vx92uw/branch/master?svg=true)](https://ci.appveyor.com/project/hunghq/hbm2code/branch/master)

## How it works

### Input [Hbm mapping files](Hbm2Code.Tests.DomainModels/Hbm)

```xml
<hibernate-mapping xmlns="urn:nhibernate-mapping-2.2">
  <class name="Worker" abstract="true">
    <id name="Id" type="Int64">
      <generator class="assigned" />
    </id>
    <discriminator column="Type" />

    <property name="Name" />
  </class>  
  
  <subclass discriminator-value="DW" name="DomesticWorker">
    <property name="SocialSecurityNo" />
  </subclass>
  
  <subclass discriminator-value="FW" name="ForeignWorker">
    <property name="PassportNo" />
  </subclass>
</hibernate-mapping>
```

### Output [Classes with Mapping by Code](Hbm2Code.Tests/Generated/GeneratedMappings.cs)

```csharp
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
```
