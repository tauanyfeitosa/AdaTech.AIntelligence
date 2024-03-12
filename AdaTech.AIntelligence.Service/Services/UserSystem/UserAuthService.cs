using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.DateLibrary.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly ILogger<UserAuthService> _logger;
        private readonly IdentityDbContext<UserInfo> _context;
        private readonly IAIntelligenceRepository<Expense> _repository;
        private IDeleteStrategy<Expense> _deleteStrategy { get; set; }


        public UserAuthService(SignInManager<UserInfo> signInManager,
            UserManager<UserInfo> userManager,
            ILogger<UserAuthService> logger, IdentityDbContext<UserInfo> context, 
            IAIntelligenceRepository<Expense> repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _repository = repository;
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
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de logout sem sucesso: {ex}");
            }
        }

        public async Task<bool> RegisterUserAsync(DTOUserRegister userRegister)
        {
            try
            {
                var userInfo = new UserInfo
                {
                    UserName = userRegister.Name,
                    Name = userRegister.Name,
                    LastName = userRegister.LastName,
                    CPF = userRegister.CPF,
                    Email = userRegister.Email,
                    DateBirth = new DateTime(userRegister.DateBirth.Year, userRegister.DateBirth.Month, userRegister.DateBirth.Day, 0, 0, 0),
                    IsStaff = true,
                };

                var result = await _userManager.CreateAsync(userInfo, userRegister.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userInfo, "Employee");
                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de registro sem sucesso com email {userRegister.Email}: {ex}");
                throw new ArgumentException($"Tentativa de registro sem sucesso: {ex}");
            }
        }

        public async Task<UserInfo> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tentativa de buscar usuário sem sucesso: {ex}");
                throw new ArgumentException($"Tentativa de buscar usuário sem sucesso: {ex}");
            }
        }

        public async Task<string> DeleteAsync(int id, bool isHardDelete)
        {
            if (isHardDelete)
                _deleteStrategy = new HardDeleteStrategy<Expense>();
            else
                _deleteStrategy = new SoftDeleteStrategy<Expense>();


            string result = await _deleteStrategy.DeleteAsync(_repository, id, _context);

            return result;
        }
    }
}
