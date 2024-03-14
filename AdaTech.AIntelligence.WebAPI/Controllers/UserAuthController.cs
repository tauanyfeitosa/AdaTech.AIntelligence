    using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Attributes;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.EmailService;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("User Authentication")]
    public class UserAuthController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly ILogger<UserAuthController> _logger;
        private readonly UserManager<UserInfo> _userManager;
        private readonly IEmailService _emailService;


        public UserAuthController(IUserAuthService userService, ILogger<UserAuthController> logger, UserManager<UserInfo> userManager, IEmailService emailService)
        {
            _userAuthService = userService;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
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
        /// Confirm the email of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("confirm-email/{userId}/{token}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] string userId, [FromRoute] string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);
            var user = await _userManager.FindByIdAsync(userId);
            try
            {
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                    if (result.Succeeded)
                    {
                        return Ok("Email confirmado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Falha ao confirmar o email.");
                    }
                }
                else
                {
                    return BadRequest("Usuário não encontrado.");
                }
            }
            catch
            {
                return BadRequest("Erro ao confirmar email.");
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
