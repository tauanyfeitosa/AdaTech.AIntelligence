
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.RoleRequirementService
{
    public interface IRoleRequirementCRUD
    {
        public Task<bool> CreateRequirements (RoleRequirement roleRequirement);
    }
}
