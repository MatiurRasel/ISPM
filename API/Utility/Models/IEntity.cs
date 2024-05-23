namespace API.Utility.Models
{
    [IgnoreEntity]
    public interface IEntity<T> : IBaseEntity
    {
        T Id { get; set; }
        bool ActiveStatus { get; set; }
    }
}
