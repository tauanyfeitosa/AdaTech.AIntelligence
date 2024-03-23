using AdaTech.AIntelligence.Service.Services.RoleRequirementService;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using AdaTech.AIntelligence.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;

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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("roles-for-promotion")]
        [Authorize]
        public IActionResult GetRolesForPromotion()
        {
            var user = _userManager.GetUserAsync(User).Result ?? throw new NotFoundException("Usuário com este ID não foi encontrado.");
            var rolesUser = _userManager.GetRolesAsync(user).Result;
            var roles = Enum.GetValues(typeof(Roles)).Cast<Roles>().ToList();
            roles = roles.Where(r => !rolesUser.Contains(r.ToString())).ToList();
            return Ok(roles);
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
            var user = await _userManager.GetUserAsync(User) ?? throw new NotFoundException("Usuário com este ID não foi encontrado.");
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
        [HttpPatch("promote-user")]
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
