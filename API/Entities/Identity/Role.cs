using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles  { get; set; }
    }
}