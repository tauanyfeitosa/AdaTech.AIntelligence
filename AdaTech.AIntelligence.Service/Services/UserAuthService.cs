using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using AdaTech.AIntelligence.Entities;

namespace AdaTech.AIntelligence.Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<UserAuth> _userManager;
        private readonly SignInManager<UserAuth> _signInManager;
        private readonly ILogger<UserAuthService> _logger;

        public UserAuthService(SignInManager<UserAuth> signInManager,
            UserManager<UserAuth> userManager,
            ILogger<UserAuthService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email,
                password, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                    return false;

                var user = await _userManager.FindByEmailAsync(email);

                _logger.LogInformation("Usuário autenticado com sucesso.");
                return user == null ? throw new ArgumentException("Usuário não encontrado.") : result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de login sem sucesso: {ex}");
                throw new ArgumentException($"Tentativa de login sem sucesso: {ex}");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            } catch(Exception ex)
            {
                _logger.LogError($"Tentativa de logout sem sucesso: {ex}");
            }
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            try
            {
                var userAuth = new UserAuth
                {
                    UserName = email,
                    Email = email,
                };

                var result = await _userManager.CreateAsync(userAuth, password);

                if (result.Succeeded)
                    await _signInManager.SignInAsync(userAuth, isPersistent: false);

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de registro sem sucesso com email {email}: {ex}");
                throw new ArgumentException($"Tentativa de registro sem sucesso: {ex}");
            }
        }
    }
}
