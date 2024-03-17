using AdaTech.AIntelligence.Configuration;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial
{
    /// <summary>
    /// Service class for managing super user creation.
    /// </summary>
    public class UserManagerService
    {
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagerService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        public UserManagerService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="userCredentialsSettings">The user credentials settings.</param>
        /// <returns>A task representing the asynchronous operation. Returns the created user if successful; otherwise, null.</returns>
        public async Task<UserInfo> CreateUserAsync(UserCredentialsSettings userCredentialsSettings)
        {
            var user = new UserInfo
            {
                UserName = userCredentialsSettings.UserName,
                Email = userCredentialsSettings.UserName,
                EmailConfirmed = true,
                IsStaff = true,
                CPF = "00000000000",
                DateBirth = DateTime.Now.AddYears(-18),
                Name = "FinancialAdmin",
                LastName = "FinancialAdmin",
                IsSuperUser = true,
            };

            var result = await _userManager.CreateAsync(user, userCredentialsSettings.Password);
            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }
    }
}
