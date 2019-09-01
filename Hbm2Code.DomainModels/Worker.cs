﻿namespace Hbm2Code.DomainModels
{
    public abstract class Worker : HasAutoGeneratedIdEntity
    {
        public virtual string Name { get; set; }
    }

    public class ForeignWorker : Worker
    {
        public virtual string PassportNo { get; set; }
    }

    public class DomesticWorker : Worker
    {
        public virtual string SocialSecurityNo { get; set; }
    }
}