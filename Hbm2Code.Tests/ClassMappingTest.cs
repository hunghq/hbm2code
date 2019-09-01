using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Configuration = NHibernate.Cfg.Configuration;

namespace Hbm2Code.Tests
{
    public class ClassMappingTest
    {
        private readonly ITestOutputHelper output;

        public ClassMappingTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void NHibernate_ShouldLoadClassMappings_AndGenerateSchema()
        {
            Configuration config = GetConfiguration();
            LoadClassMappings(config);
            GenerateSchema(config);
        }

        private void LoadClassMappings(Configuration configuration)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(GetClassMappings());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(mapping);

            output.WriteLine(mapping.AsString());
        }

        private void GenerateSchema(Configuration config)
        {
            output.WriteLine("------------------------------");
            foreach (var script in config.GenerateSchemaCreationScript(new MsSql2012Dialect()))
                output.WriteLine(script);
        }

        private static IEnumerable<Type> GetClassMappings()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == "Hbm2Code.Application")
                .GetTypes();
        }

        private static Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
                            {
                                db.ConnectionString = "";
                                db.Dialect<MsSql2012Dialect>();
                            });
            return configuration;
        }
    }
}