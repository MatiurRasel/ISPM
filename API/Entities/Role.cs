using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class Role : IdentityRole<long>
    {
        public ICollection<UserRole> UserRoles  { get; set; }
    }
}