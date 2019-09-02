using System;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Hbm2Code.Tests
{
    public class ClassMappingTemplateTest
    {
        private readonly ITestOutputHelper logger;

        public ClassMappingTemplateTest(ITestOutputHelper logger)
        {
            this.logger = logger;
        }

        [Fact]
        public void LoadAndMapAllHbmFiles_ShouldPass()
        {
            string debugFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
            var hbmFolder = Path.Combine(debugFolder, @"..\..\..\..\Hbm2Code.DomainModels\Hbm");

            foreach (var clazz in HbmLoader.LoadClassInfos(hbmFolder))
                MapClass(clazz);
        }

        private void MapClass(ClassInfo clazz)
        {
            logger.WriteLine("==================================");
            logger.WriteLine("Mapping class " + clazz.ClassName);
            logger.WriteLine("==================================");

            MapProperty(clazz.OwnProperty);

            foreach (var prop in clazz.GetChildProperties())
            {
                MapProperty(prop);
            }
        }

        private void MapProperty(Property prop)
        {
            if (prop == null)
                return;

            logger.WriteLine($"Property: {prop.Name} ({prop.TagName})");
            foreach (var attr in prop)
                logger.WriteLine($"\tm.{Mapper.MapAttributeMethod(prop, attr.Key)}({Mapper.MapAttributeValue(prop, attr.Key)});");

            foreach (var childProp in prop.ChildProperties)
                MapProperty(childProp);

            foreach (var extendedSet in prop.ExtendedPropertySets)
                foreach (var extendedProp in extendedSet)
                    MapProperty(extendedProp);
        }
    }
}