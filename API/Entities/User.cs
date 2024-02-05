using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser<long>
    {
        
        public DateTime DateOfBirth {get;set;} 
        public DateTime Created {get;set;} = DateTime.UtcNow;
        public DateTime LastActive {get;set;} = DateTime.UtcNow;
        public string Gender {get;set;}
        public string City {get;set;}
        public string Country {get;set;}
        public List<Photo> Photos {get;set;} = new();
        public ICollection<UserRole>  UserRoles { get; set; }
    }
}