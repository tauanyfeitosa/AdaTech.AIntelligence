using AdaTech.AIntelligence.Configuration;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    internal class SeedUserInitial: ISeedUserInitial
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserCredentialsSettings _userCredentialsSettings;

        public SeedUserInitial(RoleManager<IdentityRole> roleManager,
              UserManager<UserInfo> userManager,
              IOptions<UserCredentialsSettings> userCredentialsSettings)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userCredentialsSettings = userCredentialsSettings.Value;
        }

        public async Task SeedRolesAsync()
        {
            if (_userManager.FindByEmailAsync(_userCredentialsSettings.UserName).Result == null)
            {
                UserInfo user = new UserInfo
                {
                    UserName = _userCredentialsSettings.UserName,
                    Email = _userCredentialsSettings.UserName,
                    EmailConfirmed = true,
                    IsStaff = true,
                    CPF = "00000000000",
                    DateBirth = DateTime.Now.AddYears(-18),
                    Name = "FinancialAdmin",
                    LastName = "FinancialAdmin",
                    Role = Role.FinancialAdmin,
                    IsSuperUser = true,
                };

                IdentityResult result = await _userManager.CreateAsync(user, _userCredentialsSettings.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public async Task SeedUsersAsync()
        {

            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                _ = await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                _ = await _roleManager.CreateAsync(role);
            }
        }
    }
}
