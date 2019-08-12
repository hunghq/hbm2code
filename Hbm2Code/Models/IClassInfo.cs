using System;
using System.Collections.Generic;
using System.Linq;

namespace Hbm2Code
{

    public interface IClassInfo
    {
        string ClassName { get; }
        string Proxy { get; }
        string Extends { get; }
    }
}