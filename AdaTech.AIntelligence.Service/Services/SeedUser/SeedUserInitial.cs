using AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial;
using AdaTech.AIntelligence.Configuration;
using Microsoft.Extensions.Options;

namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    /// <summary>
    /// Class for seeding initial user roles.
    /// </summary>
    internal class SeedUserInitial : ISeedUserInitial
    {
        private readonly UserCredentialsSettings _userCredentialsSettings;
        private readonly RoleManagerService _roleManagerService;
        private readonly UserManagerService _userManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedUserInitial"/> class.
        /// </summary>
        /// <param name="userCredentialsSettings">The user credentials settings.</param>
        /// <param name="roleManagerService">The role manager service.</param>
        /// <param name="userManagerService">The user manager service.</param>
        public SeedUserInitial(
            IOptions<UserCredentialsSettings> userCredentialsSettings,
            RoleManagerService roleManagerService,
            UserManagerService userManagerService)
        {
            _userCredentialsSettings = userCredentialsSettings.Value;
            _roleManagerService = roleManagerService;
            _userManagerService = userManagerService;
        }

        /// <summary>
        /// Seeds initial user roles asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedRolesAsync()
        {
            var user = await _userManagerService.CreateUserAsync(_userCredentialsSettings);
            if (user != null)
            {
                await _roleManagerService.AssignRolesAsync(user, "Admin", "Finance");
            }
        }
    }
}
