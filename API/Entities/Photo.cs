

using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public long Id {get;set;}
        public string Url {get;set;}
        public bool IsMain {get;set;}
        public bool IsApproved { get; set; }
        public string PublicId {get;set;}
        public long UserId {get;set;}
        public User User {get;set;}
    }
}