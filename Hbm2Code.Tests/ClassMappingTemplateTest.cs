using Hbm2Code.Application;
using Hbm2Code.Application.Templates;
using Hbm2Code.Tests.Utils;
using System.Collections.Generic;
using System.IO;
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
            foreach (var clazz in HbmLoader.LoadClassInfos(GetHbmFolderPath()))
                MapClass(clazz);
        }

        [Fact]
        public void ExecuteRuntimeTemplate_ShouldGenerateMappingSuccessfully()
        {
            string outputFile = Path.Combine(TestUtils.GetProjectDirectory(), "Generated", "GeneratedMappings.cs");

            var template = new ClassMappingRuntime(new ClassMappingOption(
                hbmFolderPath: GetHbmFolderPath(),
                @namespace: "Hbm2Code.Tests.Generated",
                usingNamespaces: new List<string>() { "Hbm2Code.Tests.DomainModels" }));

            File.WriteAllText(outputFile, template.TransformText());
        }

        private static string GetHbmFolderPath()
        {
            return Path.Combine(TestUtils.GetProjectDirectory(), @"..\Hbm2Code.Tests.DomainModels\Hbm");
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