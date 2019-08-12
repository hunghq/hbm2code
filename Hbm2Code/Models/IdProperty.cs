namespace Hbm2Code
{
    public class IdProperty : Property
    {
        public IdProperty(IClassInfo classInfo, string name, string generatorClass) : base(classInfo, name)
        {
            TagName = "Id";
            Add("generator", generatorClass);
            GeneratorParams = new Property(classInfo, generatorClass) { TagName = "generator" };
        }

        public Property GeneratorParams { get; }
    }
}