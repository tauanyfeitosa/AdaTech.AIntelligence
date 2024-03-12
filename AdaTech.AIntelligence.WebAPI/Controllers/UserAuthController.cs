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
        private readonly ITokenService _tokenService;
        private readonly IExpenseCRUDService _expenseCRUDService;

        public UserAuthController(IUserAuthService userService, ILogger<UserAuthController> logger, ITokenService tokenService, IExpenseCRUDService expenseCRUDService)
        {
            _userAuthService = userService;
            _logger = logger;
            _tokenService = tokenService;
            _expenseCRUDService = expenseCRUDService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUserLogin userLoginInfo)
        {
            var succeeded = await _userAuthService.AuthenticateAsync(userLoginInfo.Email, userLoginInfo.Password);

            return Ok(succeeded);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userAuthService.LogoutAsync();
            _logger.LogInformation("Logout realizado com sucesso.");
            return Ok();
        }

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
    

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id, [FromQuery] bool isHardDelete = false)
        {
            var result = await _expenseCRUDService.DeleteAsync(id, isHardDelete);
            return Ok(result);
        }
    }
}
