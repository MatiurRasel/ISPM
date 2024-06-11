using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        [MaxLength(100,ErrorMessage ="Full Name Maximum Length is 100 characters.")]
        public string FullName {get;set;}
        public DateTime DateOfBirth {get;set;} 
        public DateTime Created {get;set;} = DateTime.UtcNow;
        public DateTime LastActive {get;set;} = DateTime.UtcNow;
        [StringLength(2, ErrorMessage = "Gender can only be 2 characters long.")]
        public string Gender {get;set;}
        public List<Photo> Photos {get;set;} = new();
        public ICollection<UserRole>  UserRoles { get; set; }
    }
}