using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.DbLibrary.Context;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    /// <summary>
    /// Represents the interface for delete strategies.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IDeleteStrategy<T> where T : class
    {
        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <param name="context">The database context (optional).</param>
        Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, ExpenseReportingDbContext? context = null);
    }
}
