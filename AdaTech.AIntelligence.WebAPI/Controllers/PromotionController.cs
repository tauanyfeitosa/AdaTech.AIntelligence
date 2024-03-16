using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleRequirement = AdaTech.AIntelligence.Entities.Objects.RoleRequirement;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IAIntelligenceRepository<RoleRequirement> _roleRequirementRepository;
        private readonly ILogger<PromotionController> _logger;
        private readonly UserManager<UserInfo> _userManager;
        private readonly PromotionService _promotionService;
        private readonly RequirementService _requirementService;
        public PromotionController(IAIntelligenceRepository<RoleRequirement> repository, 
            ILogger<PromotionController> logger, UserManager<UserInfo> userManager, 
            PromotionService promotionService, RequirementService requirementService)
        {
            _roleRequirementRepository = repository;
            _logger = logger;
            _userManager = userManager;
            _promotionService = promotionService;
            _requirementService = requirementService;
        }
        /// <summary>
        /// Ask for promotion
        /// </summary>
        /// 
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


        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PromoteUser(int idRequirement, Status status)
        {
            var requirement = await _promotionService.GetRequirementById(idRequirement);
            var user = await _userManager.FindByIdAsync(requirement.UserInfoId);

            if (requirement == null)
            {
                _logger.LogError($"Requisição não encontrada: {idRequirement}.");
                return BadRequest("Requisição não encontrada.");
            }

            requirement.Status = status;
            var succeeded = await _promotionService.PromotionApproval(requirement);

            if (succeeded)
            {
                if(status == Status.Approved)
                {
                    var verificacao = await _userManager.AddToRoleAsync(user, requirement.Role.ToString());
                    return Ok($"Requisição atualizada com sucesso! Usuário promovido para {requirement.Role}.");
                }
                return Ok($"Requisição atualizada com sucesso!");
            }
            else
            {
                _logger.LogError($"Aprovação sem sucesso: {requirement.UserInfo.Email}.");
                return BadRequest("Aprovação sem sucesso.");
            }
        }

    }
}
