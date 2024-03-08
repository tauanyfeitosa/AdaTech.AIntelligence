using AdaTech.AIntelligence.Configuration;
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
                    NormalizedUserName = _userCredentialsSettings.UserName.ToUpper(),
                    NormalizedEmail = _userCredentialsSettings.UserName.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsActive = true,
                    IsStaff = true,
                    IsSuperUser = true,
                    CPF = "00000000000",
                    DateBirth = DateTime.Now.AddYears(-18),
                    Name = "Admin",
                    LastName = "Admin"

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
                role.NormalizedName = "USER";
                _ = await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                _ = await _roleManager.CreateAsync(role);
            }
        }
    }
}
