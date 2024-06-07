using API.Utility.Attributes;

namespace API.Utility.Models
{
    [IgnoreEntity]
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
