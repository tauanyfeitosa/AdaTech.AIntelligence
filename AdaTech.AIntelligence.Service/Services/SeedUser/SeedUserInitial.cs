using AdaTech.AIntelligence.Configuration;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial;
using Microsoft.Extensions.Options;

namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    internal class SeedUserInitial: ISeedUserInitial
    {
        private readonly UserCredentialsSettings _userCredentialsSettings;
        private readonly RoleManagerService _roleManagerService;
        private readonly UserManagerService _userManagerService;

        public SeedUserInitial(
              IOptions<UserCredentialsSettings> userCredentialsSettings,
              RoleManagerService roleManagerService,
              UserManagerService userManagerService)
        {
            _userCredentialsSettings = userCredentialsSettings.Value;
            _roleManagerService = roleManagerService;
            _userManagerService = userManagerService;
        }

        public async Task SeedRolesAsync()
        {
            var user = await _userManagerService.CreateUserAsync(_userCredentialsSettings);
            if(user != null)
            {
                await _roleManagerService.AssignRolesAsync(user, "Admin", "Finance");
            }

            throw new UnprocessableEntityException("Erro ao criar usuário inicial.");
        }
    }
}
