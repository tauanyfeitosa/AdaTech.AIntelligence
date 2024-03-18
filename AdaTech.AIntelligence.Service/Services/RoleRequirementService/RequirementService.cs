using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.RoleRequirementService
{
    public class RequirementService
    {
        private readonly PromotionService _promotionService;
        private readonly UserManager<UserInfo> _userManager;

        public RequirementService( PromotionService promotionService, UserManager<UserInfo> userManager)
        {
            _promotionService = promotionService;
            _userManager = userManager;
        }

        public async Task<string> AskForPromotion(Roles roles, UserInfo user)
        {
            var roleRequirement = new RoleRequirement
            {
                UserInfoId = user.Id,
                Role = roles,
                Status = Status.Requested,
            };
            var succeeded = await _promotionService.PromotionRequest(roleRequirement);

            if (succeeded)
            {
                return "Promoção solicitada com sucesso!";
            }
            else
            {
                return "Solicitação sem sucesso.";
            }
        }

        public async Task<string> PromoteUser(int idRequirement, Status status)
        {
            var requirement = await _promotionService.GetRequirementById(idRequirement);
            var user = await _userManager.FindByIdAsync(requirement.UserInfoId);

            if (requirement == null)
            {
                return "Requisição não encontrada.";
            }

            requirement.Status = status;
            var succeeded = await _promotionService.PromotionApproval(requirement);

            if (succeeded)
            {
                if (status == Status.Approved)
                {
                    var verificacao = await _userManager.AddToRoleAsync(user, requirement.Role.ToString());
                    return "Requisição atualizada com sucesso! Usuário promovido para {requirement.Role}.";
                }
                return "Requisição atualizada com sucesso!";
            }
            else
            {
                return "Aprovação sem sucesso.";
            }
        }
    }
}
