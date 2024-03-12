using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly ILogger<UserAuthController> _logger;
        private readonly ITokenService _tokenService;

        public UserAuthController(IUserAuthService userService, ILogger<UserAuthController> logger, ITokenService tokenService)
        {
            _userAuthService = userService;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUserLogin userLoginInfo)
        {
            var succeeded = await _userAuthService.AuthenticateAsync(userLoginInfo.Email, userLoginInfo.Password);

            return Ok(succeeded);
        }

        [HttpPost("logout")]
        //[Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userAuthService.LogoutAsync();
            _logger.LogInformation("Logout realizado com sucesso.");
            return Ok();
        }

        [HttpPost("createUser")]
        //[Authorize]
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id, [FromQuery] bool isHardDelete = false)
        {
            var result = await _userAuthService.DeleteAsync(id, isHardDelete);
            return Ok(result);
        }
    }
}
