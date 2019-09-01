namespace Hbm2Code
{
    public interface IClassInfo
    {
        string ClassName { get; }
        string Proxy { get; }
        string Extends { get; }
    }
}