using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services
{
    public class GenericDeleteService<T> where T: class 
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, IdentityDbContext<UserInfo>? context, int id, bool hardDelete)
        {
            IDeleteStrategy<T> strategy = hardDelete ? new HardDeleteStrategy<T>() : new SoftDeleteStrategy<T>();
            return await strategy.DeleteAsync(repository, id, context);
        }
    }
}
