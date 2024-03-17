using AdaTech.AIntelligence.Attributes;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("Promotion User")]
    [TypeFilter(typeof(LoggingActionFilter))]
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly UserManager<UserInfo> _userManager;
        private readonly RequirementService _requirementService;
        public PromotionController(ILogger<PromotionController> logger, UserManager<UserInfo> userManager, 
            RequirementService requirementService)
        {
            _logger = logger;
            _userManager = userManager;
            _requirementService = requirementService;
        }

        /// <summary>
        /// Ask for promotion
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost("ask-for-promotion")]
        [Authorize]
        public async Task<IActionResult> AskForPromotion(Roles roles)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var result = await _requirementService.AskForPromotion(roles, user);

            if (result == "Promoção solicitada com sucesso!")
            {
                return Ok(result);
            }
            else
            {
                _logger.LogError($"Solicitação sem sucesso: {user.Email}.");
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Promote user
        /// </summary>
        /// <param name="idRequirement"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PromoteUser(int idRequirement, Status status)
        {
            var result = await _requirementService.PromoteUser(idRequirement, status);

            if (result != "Aprovação sem sucesso.")
            {
                return Ok(result);
            }
            else
            {
                _logger.LogError($"Solicitação sem sucesso: {idRequirement}.");
                return BadRequest(result);
            }
        }

    }
}
