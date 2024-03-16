using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.UserSystem.PromotionServices
{
    public class PromotionService
    {
        public async Task<bool> PromotionRequest(RoleRequirement roleRequirement, IAIntelligenceRepository<RoleRequirement> repository)
        {
            
            roleRequirement.RequestDate = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Requested;
            return await repository.Create(roleRequirement);
        }

        public async Task<bool> PromotionApproval(RoleRequirement roleRequirement, IAIntelligenceRepository<RoleRequirement> repository)
        {
            roleRequirement.ApprovalDate = DateTime.Now;
            roleRequirement.UpdateAt = DateTime.Now;
            roleRequirement.Status = Entities.Enums.Status.Approved;
            return await repository.Update(roleRequirement);
        }

        public async Task<RoleRequirement> GetRequirementById(int id, IAIntelligenceRepository<RoleRequirement> repository)
        {
            return await repository.GetOne(id);
        }
    }
}
