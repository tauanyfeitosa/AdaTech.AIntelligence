using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services.UserSystem.IUserPromote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPromotionController : ControllerBase
    {
        private readonly IUserPromoteService _userPromoteService;
        private readonly IUserAuthService _userAuthService;

        public UserPromotionController(IUserPromoteService userPromoteService, IUserAuthService userAuthService)
        {
            _userPromoteService = userPromoteService;
            _userAuthService = userAuthService;
        }

        [Authorize]
        [TypeFilter(typeof(AcessAdminFilter))]
        [HttpPost("promoteUser")]
        public async Task<IActionResult> PromoteUser([FromBody]string email)
        {
            var user = await _userAuthService.GetUserByEmailAsync(email);

            if (user == null)
                return BadRequest("Usuário não encontrado.");

            var success = await _userPromoteService.PromoteUser(email);

            if (!success)
                return BadRequest("Erro ao promover usuário.");

            return Ok("Usuário promovido com sucesso!");
        }

        [HttpPost("requestPromotion")]
        public async Task<IActionResult> RequestPromotion()
        {
            var email = User.Identity.Name;

            var success = await _userPromoteService.RequestPromotion(email);

            if (!success)
                return BadRequest("Erro ao solicitar promoção.");

            return Ok("Solicitação de promoção realizada com sucesso!");
        }
    }
    
}
