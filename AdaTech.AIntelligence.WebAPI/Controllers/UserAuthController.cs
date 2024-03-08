using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs;
using AdaTech.AIntelligence.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserAuthController> _logger;

        public UserAuthController(IUserAuthService userService, ITokenService tokenService, ILogger<UserAuthController> logger)
        {
            _userAuthService = userService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUserLogin userLoginInfo)
        {
            var succeeded = await _userAuthService.AuthenticateAsync(userLoginInfo.Email, userLoginInfo.Password);

            if (succeeded)
            {
                UserInfo user = new UserInfo()
                {
                    Email = userLoginInfo.Email,
                    PasswordHash = userLoginInfo.Password,
                };

                var (Token, Expiration) = _tokenService.GenerateToken(user);

                _logger.LogInformation($"Usuário logado com sucesso: {userLoginInfo.Email}.");
                return Ok(new DTOUserToken(Token, Expiration));
            }
            else
            {
                _logger.LogError($"Login de usuário sem sucesso: {userLoginInfo.Email}.");
                return BadRequest("Login de usuário sem sucesso.");
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userAuthService.LogoutAsync();
            _logger.LogInformation("Logout realizado com sucesso.");
            return Ok();
        }

        [HttpPost("createUser")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Register([FromBody] DTOUserLogin userInfo)
        {
            var succeeded = await _userAuthService.RegisterUserAsync(userInfo.Email, userInfo.Password);

            if (succeeded)
            {
                _logger.LogInformation($"Usuário criado com sucesso: {userInfo.Email}");
                return Ok($"Usário {userInfo.Email} foi criado com sucesso!");
            }
            else
            {
                _logger.LogError($"Login inválido: {userInfo.Email}.");
                return BadRequest("Login inválido.");
            }
        }
    }
}
