using System.Collections.Generic;

namespace Hbm2Code.Application
{
    public class ClassMappingOption
    {
        public ClassMappingOption(string hbmFolderPath, string @namespace, IList<string> usingNamespaces)
        {
            HbmFolderPath = hbmFolderPath;
            Namespace = @namespace;
            UsingNamespaces = usingNamespaces ?? new List<string>();
        }

        public string HbmFolderPath { get; }
        public string Namespace { get; }
        public IList<string> UsingNamespaces { get; }
    }
}