using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Role : IdentityRole<long>
    {
        public ICollection<UserRole> UserRoles  { get; set; }
    }
}