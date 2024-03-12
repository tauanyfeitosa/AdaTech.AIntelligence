using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public interface IDeleteStrategy<T> where T : class
    {
        Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, int id, IdentityDbContext<UserInfo>? context = null);
    }
}
