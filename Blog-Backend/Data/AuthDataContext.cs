using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_Backend.Data
{
    public class AuthDataContext : IdentityDbContext
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "7327ae3c-52c5-455a-b8c6-c206d1f776dc";
            var writerRoleId = "30082515-155d-4b8b-b7c8-bbbddf835818";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                      Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            var adminUserId = "3dd3fa83-5ea9-4fbf-b6a1-a312dfe22ba6";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId

                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

            var userId = "667c747c-4e56-4e1b-af94-f498f24de516";
            var user = new IdentityUser()
            {
                Id = userId,
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                NormalizedEmail = "user@gmail.com".ToUpper(),
                NormalizedUserName = "user@gmail.com".ToUpper()
            };

            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "User@123");

            builder.Entity<IdentityUser>().HasData(user);

            var userRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = userId,
                    RoleId = readerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
