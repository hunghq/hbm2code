using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Hbm2Code
{
    public static class HbmLoader
    {
        public static IList<ClassInfo> LoadClassInfos(string hbmFolderPath, string searchPattern = "*.hbm.xml")
        {
            var result = new List<ClassInfo>();

            Console.WriteLine("Reading hbm files from " + hbmFolderPath);
            foreach (string file in Directory.GetFiles(hbmFolderPath, searchPattern, SearchOption.AllDirectories))
            {
                var parser = new HbmParser(XDocument.Load(file));
                result.AddRange(parser.Parse());
            }

            return result;
        }
    }
}