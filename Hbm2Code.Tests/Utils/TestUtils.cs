using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Hbm2Code.Tests.Utils
{
    internal static class TestUtils
    {
        internal static IList<ClassInfo> ParseHbm(string fileName)
        {
            return new HbmParser(XDocument.Load(GetHbmFile(fileName))).Parse();
        }

        internal static ClassInfo ParseHbm(string fileName, string className, ClassType classType)
        {
            return ParseHbm(fileName).AssertHasClass(className, classType);
        }

        internal static string GetHbmFile(string fileName)
        {
            string testProjectFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
            return Path.Combine(testProjectFolder, "Hbm", fileName);
        }
    }
}