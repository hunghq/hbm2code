using System.Xml.Linq;

namespace Hbm2Code.Parsers
{
    public interface IPropertyParser
    {
        string TagName { get; }

        Property Parse(ClassInfo clazz, XElement element);
    }
}