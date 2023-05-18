using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Core.Models.Test;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Identity;

namespace LeavePlanner.Persistence
{
    public static class Seed
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // SEED ROLES
            if (!roleManager.RoleExistsAsync(UserRoles.Administrator).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = UserRoles.Administrator;
                roleManager.CreateAsync(role).Wait();
            }

            // SEED USERS
            if (userManager.FindByEmailAsync("admin@admin.hr").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@admin.hr",
                    Email = "admin@admin.hr",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "Lozinka.1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.Administrator).Wait();
                }
            }
        }

        public static void SeedData(ApplicationDbContext _context)
        {
            if (!_context.TestTables.Any())
            {
                var testTable = new TestTable();
                testTable.CultureName = "AAA";
                testTable.CultureCategory = "BBB";
                testTable.SamplesMultiplier = 1;

                _context.TestTables.Add(testTable);
                _context.SaveChanges();
            }
        }
    }
}
