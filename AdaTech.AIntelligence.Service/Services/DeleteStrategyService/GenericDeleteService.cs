using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class GenericDeleteService<T> where T : class
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, int id, bool hardDelete, IdentityDbContext<UserInfo>? context = null)
        {
            IDeleteStrategy<T> strategy = hardDelete ? new HardDeleteStrategy<T>() : new SoftDeleteStrategy<T>();
            return await strategy.DeleteAsync(repository, id, context);
        }
    }
}
