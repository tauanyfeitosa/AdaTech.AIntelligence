using AdaTech.AIntelligence.Configuration;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial
{
    public class UserManagerService
    {
        private readonly UserManager<UserInfo> _userManager;

        public UserManagerService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

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
            if(result.Succeeded)
            {
                return user;
            }

            return null;
        }
    }
}
