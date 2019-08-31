namespace Hbm2Code.Tests.Utils
{
    public static class TestAssertions
    {
        public static PropertyAssertions Should(this Property instance)
        {
            return new PropertyAssertions(instance);
        }
    }
}