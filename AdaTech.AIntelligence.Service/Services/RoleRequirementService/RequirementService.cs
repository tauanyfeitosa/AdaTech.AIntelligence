using AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace AdaTech.AIntelligence.Service.Services.RoleRequirementService
{
    /// <summary>
    /// Service class for managing role requirement requests and promotions.
    /// </summary>
    public class RequirementService
    {
        private readonly PromotionService _promotionService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly HttpContext _httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementService"/> class.
        /// </summary>
        /// <param name="promotionService">The promotion service instance.</param>
        /// <param name="userManager">The user manager instance.</param>
        public RequirementService( PromotionService promotionService, UserManager<UserInfo> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _promotionService = promotionService;
            _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        /// Sends a promotion request for a user asynchronously.
        /// </summary>
        /// <param name="roles">The desired role for promotion.</param>
        /// <param name="user">The user to promote.</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the promotion request.</returns>
        public async Task<string> AskForPromotion(Roles roles, UserInfo user)
        {
            if  (roles == 0)
            {
                throw new UnprocessableEntityException("Cargo invalída.");
            }
            var userContext = _httpContext.User;
            var isRole = userContext.IsInRole(roles.ToString());

            if (isRole)
            {
                throw new InvalidOperationException("Cargo inválido, você já possui essa permissão");
            }

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
                throw new UnprocessableEntityException ("Erro ao solicitar promoção.");
            }
        }

        /// <summary>
        /// Promotes a user based on a given requirement ID and status asynchronously.
        /// </summary>
        /// <param name="idRequirement">The ID of the promotion requirement.</param>
        /// <param name="status">The status of the promotion (e.g., Approved or Rejected).</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the promotion.</returns>
        public async Task<string> PromoteUser(int idRequirement, Status status)
        {
            var requirement = await _promotionService.GetRequirementById(idRequirement);
            var user = await _userManager.FindByIdAsync(requirement.UserInfoId);

            if (requirement == null)
            {
                throw new NotFoundException("Requisição não encontrada.");
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
                throw new UnprocessableEntityException("Erro ao atualizar requisição.");
            }
        }
    }
}
