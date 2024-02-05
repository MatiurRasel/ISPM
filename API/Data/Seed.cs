using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
            {
                if( await userManager.Users.AnyAsync()) return;
                var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                };
                var users = JsonSerializer.Deserialize<List<User>>(userData);

                var roles =  new List<Role>
                {
                    new Role {Name = "Super Admin"},
                    new Role {Name = "Admin"},
                    new Role {Name = "Moderator"},
                    new Role {Name = "User"},
                    new Role {Name = "Junior User"}
                };

                foreach(var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                foreach(var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    user.Created = DateTime.SpecifyKind(user.Created,DateTimeKind.Utc); 
                    user.LastActive = DateTime.SpecifyKind(user.LastActive,DateTimeKind.Utc); 
                    await userManager.CreateAsync(user,"Pa$$w0rd");
                    await userManager.AddToRoleAsync(user,"User");
                }

                // var sadmin =  new User
                // {
                //     UserName = "sadmin"
                // };

                // await userManager.CreateAsync(sadmin,"aA0202");
                // await userManager.AddToRolesAsync(sadmin,new []{"Admin"});

                var admin =  new User
                {
                    UserName = "admin"
                };

                await userManager.CreateAsync(admin,"aA0202");
                await userManager.AddToRolesAsync(admin,new []{"Admin","Super Admin","Moderator"});

                //await context.SaveChangesAsync();
            }
    }
}