using AdaTech.AIntelligence.Ioc.Filters;
using AdaTech.AIntelligence.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIntelligenceController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AIntelligenceController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("criarToken")]
        public IActionResult CriarToken(string user, string password)
        {
            var token = _tokenService.GenerateToken(user, password);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = System.DateTime.Now.AddHours(1)
            };
            Response.Cookies.Append("jwt", token, cookieOptions);
            return Ok(token);
        }

        [HttpPost("deletarToken")]
        [ServiceFilter(typeof(MustHaveAToken))]
        public IActionResult DeletarToken()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Token deletado");
        }
    }
}
