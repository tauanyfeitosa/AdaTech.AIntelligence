using AdaTech.AIntelligence.DataLibrary.Context;
using AdaTech.AIntelligence.DataLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    public interface IDeleteStrategy<T> where T : class
    {
        Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, int id, ExpenseReportingDbContext? context = null);
    }
}
