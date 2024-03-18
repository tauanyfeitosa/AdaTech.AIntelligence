using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial
{
    /// <summary>
    /// Service class for managing user roles.
    /// </summary>
    public class RoleManagerService
    {
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagerService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        public RoleManagerService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Assigns roles to a user asynchronously.
        /// </summary>
        /// <param name="user">The user to assign roles to.</param>
        /// <param name="roles">The roles to assign.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AssignRolesAsync(UserInfo user, params string[] roles)
        {
            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
