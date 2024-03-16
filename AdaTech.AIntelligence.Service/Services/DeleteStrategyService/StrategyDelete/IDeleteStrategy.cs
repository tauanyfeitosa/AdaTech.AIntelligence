using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    public interface IDeleteStrategy<T> where T : class
    {
        Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, ExpenseReportingDbContext? context = null);
    }
}
