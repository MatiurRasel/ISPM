namespace API.Utility.Models
{
    [IgnoreEntity]
    public interface IAuditableEntity
    {
        int CreateUserID { get; set; }
        DateTime CreateTime { get; set; }
        int UpdateUserID { get; set; }
        DateTime UpdateTime { get; set; }
        bool ActiveStatus { get; set; }
        string CreateUserIPAddress { get; set; }
        string UpdateUserIPAddress { get; set; }
    }
}
