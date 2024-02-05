using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<User,Role,long,
    IdentityUserClaim<long>,UserRole,IdentityUserLogin<long>,
    IdentityRoleClaim<long>,IdentityUserToken<long>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        //public DbSet<CountryInfo> CountryInfos {get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Remove "AspNet" prefix from table names
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");

            builder.Entity<User>()
                .HasMany(ur=>ur.UserRoles)
                .WithOne(u=>u.User)
                .HasForeignKey(ur=>ur.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(ur=>ur.UserRoles)
                .WithOne(u=>u.Role)
                .HasForeignKey(ur=>ur.RoleId)
                .IsRequired();   

        }
    }
}