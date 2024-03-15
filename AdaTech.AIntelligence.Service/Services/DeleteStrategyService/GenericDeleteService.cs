using AdaTech.AIntelligence.DataLibrary.Context;
using AdaTech.AIntelligence.DataLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class GenericDeleteService<T> where T : class
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, int id, bool hardDelete, ExpenseReportingDbContext? context = null)
        {
            IDeleteStrategy<T> strategy = hardDelete ? new HardDeleteStrategy<T>() : new SoftDeleteStrategy<T>();
            return await strategy.DeleteAsync(repository, id, context);
        }
    }
}
