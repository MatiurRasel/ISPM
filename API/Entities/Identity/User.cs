using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName {get;set;}
        public DateTime DateOfBirth {get;set;} 
        public DateTime Created {get;set;} = DateTime.UtcNow;
        public DateTime LastActive {get;set;} = DateTime.UtcNow;
        public string Gender {get;set;}
        public List<Photo> Photos {get;set;} = new();
        public ICollection<UserRole>  UserRoles { get; set; }
    }
}