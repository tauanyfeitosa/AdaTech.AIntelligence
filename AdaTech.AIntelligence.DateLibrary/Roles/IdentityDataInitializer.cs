using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.DateLibrary.Roles
{
    /// <summary>
    /// Class to initialize the roles in the application
    /// </summary>
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<UserInfo> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
        }

        /// <summary>
        /// Create Roles for the application based on the roles in the database
        /// </summary>
        /// <param name="roleManager"></param>
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Finance").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Finance";
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                roleManager.CreateAsync(role).Wait();
            }
        }
    }

}
