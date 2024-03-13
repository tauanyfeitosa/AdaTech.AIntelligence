using AdaTech.AIntelligence.Service.Attributes;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("User Authentication")]
    public class UserAuthController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly ILogger<UserAuthController> _logger;

        public UserAuthController(IUserAuthService userService, ILogger<UserAuthController> logger)
        {
            _userAuthService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="userLoginInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUserLogin userLoginInfo)
        {
            var succeeded = await _userAuthService.AuthenticateAsync(userLoginInfo.Email, userLoginInfo.Password);

            return Ok(succeeded);
        }

        /// <summary>
        /// Logout from the system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userAuthService.LogoutAsync();
            _logger.LogInformation("Logout realizado com sucesso.");
            return Ok();
        }

        /// <summary>
        /// Register a new user withouth roles
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        [HttpPost("createUser")]
        public async Task<IActionResult> Register([FromBody] DTOUserRegister userRegister)
        {
            var succeeded = await _userAuthService.RegisterUserAsync(userRegister);

            if (succeeded)
            {
                _logger.LogInformation($"Usuário criado com sucesso: {userRegister.Email}");
                return Ok($"Usário {userRegister.Email} foi criado com sucesso!");
            }
            else
            {
                _logger.LogError($"Registro sem sucesso: {userRegister.Email}.");
                return BadRequest("Registro sem sucesso.");
            }
        }

        /// <summary>
        /// Register a new super user
        /// </summary>
        /// <param name="dTOSuperUserRegister"></param>
        /// <returns></returns>
        [HttpPost("create-super-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterSuperUser([FromBody] DTOSuperUserRegister dTOSuperUserRegister)
        {
            var succeeded = await _userAuthService.RegisterUserAsync(dTOSuperUserRegister);

            if (succeeded)
            {
                _logger.LogInformation($"Usuário criado com sucesso: {dTOSuperUserRegister.Email}");
                return Ok($"Usário {dTOSuperUserRegister.Email} foi criado com sucesso!");
            }
            else
            {
                _logger.LogError($"Registro sem sucesso: {dTOSuperUserRegister.Email}.");
                return BadRequest("Registro sem sucesso.");
            }
        }
    }
}
