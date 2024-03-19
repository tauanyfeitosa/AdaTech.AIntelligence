using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.DbLibrary.Roles
{
    /// <summary>
    /// Class to initialize the roles in the application.
    /// </summary>
    public static class IdentityDataInitializer
    {
        /// <summary>
        /// Seeds data for roles in the application.
        /// </summary>
        /// <param name="roleManager">The role manager instance.</param>
        public static void SeedData(RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
        }

        /// <summary>
        /// Creates roles for the application based on the roles in the database.
        /// </summary>
        /// <param name="roleManager">The role manager instance.</param>
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Admin"
                };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Finance").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Finance"
                };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Employee"
                };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
