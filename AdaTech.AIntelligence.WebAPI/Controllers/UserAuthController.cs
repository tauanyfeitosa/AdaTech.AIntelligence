using AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Authorization;
using AdaTech.AIntelligence.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("User Authentication")]
    [TypeFilter(typeof(LoggingActionFilter))]
    public class UserAuthController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly UserManager<UserInfo> _userManager;

        public UserAuthController(IUserAuthService userService, UserManager<UserInfo> userManager)
        {
            _userAuthService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="userLoginInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUserLogin userLoginInfo)
        {
            var succeeded = await _userAuthService.AuthenticateAsync(userLoginInfo.Email!, userLoginInfo.Password!);

            if (!succeeded)
                throw new UnauthorizedAccessException("Login sem sucesso.");

            return Ok(new { message = $"Usuário {userLoginInfo.Email} logado com sucesso!" });
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
            return Ok("Usuário deslogado com sucesso!");
        }

        /// <summary>
        /// Register a new user withouth roles
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public async Task<IActionResult> Register([FromBody] DTOUserRegister userRegister)
        {
            var succeeded = await _userAuthService.RegisterUserAsync(userRegister);

            if (succeeded)
                return Ok(new { message = $"Usário {userRegister.Email} foi criado com sucesso!" });
            else
                return BadRequest(new { message = "Registro sem sucesso." });
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
                return Ok($"Usário {dTOSuperUserRegister.Email} foi criado com sucesso!");
            else
                return BadRequest("Registro sem sucesso.");
        }
    }
}
