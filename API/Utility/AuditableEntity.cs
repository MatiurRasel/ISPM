using API.Utility.Attributes;
using API.Utility.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Utility
{
    [IgnoreEntity]
    public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        protected AuditableEntity()
        {
            CreateUserID = 0;
            CreateTime = DateTime.Now;
            ActiveStatus = true;
            UpdateUserID = 0;
            UpdateTime = DateTime.Now;
            CreateUserIPAddress = "127.0.0.1";
            UpdateUserIPAddress = "127.0.0.1";
        }
        [IgnoreUpdate]
        [ScaffoldColumn(false)]
        public int CreateUserID { get; set; }
        [IgnoreUpdate]
        [ScaffoldColumn(false)]
        public DateTime CreateTime { get  ; set  ; }
        [ScaffoldColumn(false)]
        public int UpdateUserID { get  ; set  ; }
        [ScaffoldColumn(false)]
        public DateTime UpdateTime { get  ; set  ; }
        [StringLength(256)]
        public string CreateUserIPAddress { get  ; set  ; }
        [StringLength(256)]
        public string UpdateUserIPAddress { get  ; set  ; }
    }
}
