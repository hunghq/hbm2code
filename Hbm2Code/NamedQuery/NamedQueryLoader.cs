using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hbm2Code
{
    public static class NamedQueryLoader
    {
        private static ISet<string> NamedQueryTags = new HashSet<string>() { "query", "sql-query" };

        public static IList<XElement> LoadQueries(string hbmFolderPath)
        {
            var result = new List<XElement>();

            Console.WriteLine("Reading hbm files from " + hbmFolderPath);
            foreach (string file in Directory.GetFiles(hbmFolderPath, "*.hbm.xml", SearchOption.AllDirectories))
            {
                XDocument hbmDocument = XDocument.Load(file);
                var queries = hbmDocument.Root.Elements()
                    .Where(x => NamedQueryTags.Contains(x.Name.LocalName))
                    .ToList();

                foreach (var query in queries)
                    result.Add(RemoveNamespace(query));
            }

            return result;
        }

        private static XElement RemoveNamespace(XElement element)
        {
            element.Name = element.Name.LocalName;
            foreach (var child in element.Elements())
                RemoveNamespace(child);

            return element;
        }
    }
}