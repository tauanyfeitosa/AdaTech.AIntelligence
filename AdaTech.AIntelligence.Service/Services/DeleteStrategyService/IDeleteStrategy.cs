using AdaTech.AIntelligence.DateLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public interface IDeleteStrategy<T> where T : class
    {
        Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, ILogger logger, DbContext context, int id);
    }
}
