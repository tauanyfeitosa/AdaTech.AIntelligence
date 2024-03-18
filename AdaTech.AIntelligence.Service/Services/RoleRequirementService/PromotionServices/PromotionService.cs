using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices
{
    /// <summary>
    /// Service class for handling promotion requests and approvals.
    /// </summary>
    public class PromotionService
    {
        private readonly IAIntelligenceRepository<RoleRequirement> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionService"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing role requirement data.</param>
        public PromotionService(IAIntelligenceRepository<RoleRequirement> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Submits a promotion request asynchronously.
        /// </summary>
        /// <param name="roleRequirement">The role requirement object representing the promotion request.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the request is successfully submitted; otherwise, false.</returns>
        public async Task<bool> PromotionRequest(RoleRequirement roleRequirement)
        {
            roleRequirement.RequestDate = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Requested;
            return await _repository.Create(roleRequirement);
        }

        /// <summary>
        /// Approves a promotion request asynchronously.
        /// </summary>
        /// <param name="roleRequirement">The role requirement object representing the promotion request.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the request is successfully approved; otherwise, false.</returns>
        public async Task<bool> PromotionApproval(RoleRequirement roleRequirement)
        {
            roleRequirement.ApprovalDate = DateTime.Now;
            roleRequirement.UpdateAt = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Approved;
            return await _repository.Update(roleRequirement);
        }

        /// <summary>
        /// Retrieves a role requirement by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the role requirement to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the role requirement object if found; otherwise, null.</returns>
        public async Task<RoleRequirement> GetRequirementById(int id)
        {
            return await _repository.GetOne(id);
        }
    }
}
