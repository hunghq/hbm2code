using Hbm2Code.Application.Templates;
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
            string hbmFolder = GetHbmFolderPath();

            foreach (var clazz in HbmLoader.LoadClassInfos(hbmFolder))
                MapClass(clazz);
        }

        [Fact]
        public void ExecuteRuntimeTemplate_ShouldGenerateMappingSuccessfully()
        {
            string output = Path.Combine(GetBuildDirectory(), $"{nameof(ClassMappingRuntime)}.cs");
            ClassMappingRuntime template = new ClassMappingRuntime(GetHbmFolderPath());
            File.WriteAllText(output, template.TransformText());
        }

        private static string GetHbmFolderPath()
        {
            return Path.Combine(GetBuildDirectory(), @"..\..\..\..\Hbm2Code.DomainModels\Hbm");
        }

        private static string GetBuildDirectory()
        {
            return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
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