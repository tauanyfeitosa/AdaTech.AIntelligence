using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.UserSystem.PromotionServices
{
    public class PromotionService
    {
        private readonly IAIntelligenceRepository<RoleRequirement> _repository;

        public PromotionService(IAIntelligenceRepository<RoleRequirement> repository)
        {
            _repository = repository;
        }

        public async Task<bool> PromotionRequest(RoleRequirement roleRequirement)
        {
            
            roleRequirement.RequestDate = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Requested;
            return await _repository.Create(roleRequirement);
        }

        public async Task<bool> PromotionApproval(RoleRequirement roleRequirement)
        {
            roleRequirement.ApprovalDate = DateTime.Now;
            roleRequirement.UpdateAt = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Approved;
            return await _repository.Update(roleRequirement);
        }

        public async Task<RoleRequirement> GetRequirementById(int id)
        {
            return await _repository.GetOne(id);
        }
    }
}
