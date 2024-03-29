using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface;
using AdaTech.AIntelligence.Service.Services.EmailService;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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
            var result = await _signInManager.PasswordSignInAsync(email,
            password, false, lockoutOnFailure: false);

            var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException("Usuário não encontrado. Por favor, realize o autocadastro!");
            if (!user.EmailConfirmed)
                throw new UnauthorizedAccessException("E-mail não confirmado. Por favor, confirme seu e-mail.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Usuário inativo. Por favor, entre em contato com o administrador.");

            _logger.LogInformation("Usuário autenticado com sucesso.");
            return result.Succeeded;
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
            await ValidateCpfAsync(userRegister.CPF!);
            await ValidateEmailAsync(userRegister.Email!);

            var userInfo = await userRegister.RegisterUserAsync();
            userInfo.LockoutEnabled = false;
            var result = await _userManager.CreateAsync(userInfo, userRegister.Password!);
            if (!result.Succeeded)
            {
                throw new UnprocessableEntityException ("Falha ao criar usuário.");
            }

            await AssignRolesAsync(userInfo, userRegister);
            await SendConfirmationEmailAsync(userInfo);

            return result.Succeeded;
        }

        /// <summary>
        /// Confirms the email of a user asynchronously.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        /// <exception cref="UnprocessableEntityException"></exception>
        private async Task ValidateCpfAsync(string cpf)
        {
            var userCpf = await _userManager.Users.FirstOrDefaultAsync(u => u.CPF == cpf);
            if (userCpf != null)
                throw new UnprocessableEntityException("CPF já cadastrado.");
        }

        /// <summary>
        /// Confirms the email of a user asynchronously.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="UnprocessableEntityException"></exception>
        private async Task ValidateEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                throw new UnprocessableEntityException("Usuário já cadastrado.");
        }

        /// <summary>
        /// Assigns roles to a user asynchronously.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        private async Task AssignRolesAsync(UserInfo userInfo, IUserRegister userRegister)
        {
            if (userRegister is DTOSuperUserRegister superUserRegister)
            {
                foreach (var role in superUserRegister.Roles!)
                {
                    await _userManager.AddToRoleAsync(userInfo, role.ToString());
                }
            }
            else
            {
                await _userManager.AddToRoleAsync(userInfo, "Employee");
            }
        }

        /// <summary>
        /// Sends a confirmation email to a user asynchronously.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private async Task SendConfirmationEmailAsync(UserInfo userInfo)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userInfo);
            var confirmationLink = $"{_appSettings.GetValue<string>("ServerSMTP:BaseUrl")}/{userInfo.Id}/{Uri.EscapeDataString(token)}";
            var emailBody = $"Por favor, clique no link a seguir para confirmar seu endereço de e-mail: <a href='{confirmationLink}'>Confirmar E-mail</a>";

            await _emailService.SendEmailAsync(userInfo.Email!, "Confirmação de E-mail", emailBody);
        }
    }
}
