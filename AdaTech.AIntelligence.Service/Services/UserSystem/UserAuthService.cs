using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AdaTech.AIntelligence.Service.Services.EmailService;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    /// <summary>
    /// Service responsible for user authentication and registration.
    /// </summary>
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly ILogger<UserAuthService> _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthService"/> class.
        /// </summary>
        /// <param name="signInManager">The sign-in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="appSettings">The application settings.</param>
        public UserAuthService(SignInManager<UserInfo> signInManager,
            UserManager<UserInfo> userManager,
            ILogger<UserAuthService> logger,
            IEmailService emailService, IConfiguration appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Authenticates a user with the provided email and password asynchronously.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the authentication is successful; otherwise, false.</returns>
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
                return user == null ? throw new NotFoundException("Usuário não encontrado.") : result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de login sem sucesso: {ex}");
                throw new InvalidOperationException($"Tentativa de login sem sucesso: {ex}");
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de logout sem sucesso: {ex}");
                throw new InvalidOperationException($"Tentativa de logout sem sucesso: {ex}");
            }
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="userRegister">The user registration information.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the registration is successful; otherwise, false.</returns>
        public async Task<bool> RegisterUserAsync(IUserRegister userRegister)
        {
            try
            {
                var userInfo = await userRegister.RegisterUserAsync();

                var result = await _userManager.CreateAsync(userInfo, userRegister.Password);

                if (result.Succeeded)
                {
                    if (userRegister is DTOSuperUserRegister superUserRegister)
                    {
                        foreach (var item in superUserRegister.Roles)
                        {
                            await _userManager.AddToRoleAsync(userInfo, item.ToString());
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userInfo, "Employee");
                    }

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userInfo);

                    var confirmationLink = $"{_appSettings.GetValue<string>("ServerSMTP:BaseUrl")}/{userInfo.Id}/{Uri.EscapeDataString(token)}";

                    var emailBody = $"Por favor, clique no link a seguir para confirmar seu endereço de e-mail: <a href='{confirmationLink}'>Confirmar E-mail</a>";

                    await _emailService.SendEmailAsync(userInfo.Email, "Confirmação de E-mail", emailBody);
                }
                return result.Succeeded;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de registro sem sucesso com email {userRegister.Email}: {ex}");
                throw new UnprocessableEntityException($"Tentativa de registro sem sucesso: {ex}");
            }
        }
    }
}
