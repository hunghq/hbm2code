namespace Hbm2Code.DomainModels
{
    public class BaseObject
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual long Version { get; protected set; }
    }
}
