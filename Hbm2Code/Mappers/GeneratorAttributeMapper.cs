using System;

namespace Hbm2Code
{
    public class GeneratorAttributeMapper : AttributeMapper
    {
        public GeneratorAttributeMapper() : base(valueMapper: MapGenerator)
        {
        }

        public static string MapGenerator(Property property, string generatorClass)
        {
            var idProperty = property as IdProperty;
            if (idProperty == null)
                throw new ArgumentException($"Property {property.Name} in class " +
                    $"{property.ClassInfo.ClassName} must be a IdProperty to map generator attribute");

            switch (generatorClass)
            {
                case "assigned":
                    return "Generators.Assigned";

                case "foreign":
                    return $"Generators.Foreign<{idProperty.ClassInfo.ClassName}>(y => y.{GetForeignKey(idProperty)})";

                default:
                    throw new ArgumentException($"Class {idProperty.ClassInfo.ClassName} has unsupported Id generator: " + generatorClass);
            }
        }

        private static string GetForeignKey(IdProperty idProperty)
        {
            return idProperty.GeneratorParams.ContainsKey("property")
                ? idProperty.GeneratorParams["property"]
                : throw new ArgumentException(
                    $"Id Property in class {idProperty.ClassInfo.ClassName} do not have foreign key class defined");
        }
    }
}