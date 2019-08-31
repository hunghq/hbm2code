﻿using System;

namespace Hbm2Code.DomainModels
{
    public class Agency : BaseObject
    {
        public Agency()
        {

        }

        public virtual Guid Guid { get; set; }
        public virtual Area Area { get; set; }
    }

    public class HeadQuarter : HasAutoGeneratedIdEntity
    {
        public HeadQuarter()
        {

        }

        public virtual string Name { get; set; }

        public virtual Agency Agency { get; set; }
    }
}
