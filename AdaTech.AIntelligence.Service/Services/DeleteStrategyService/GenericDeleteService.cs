using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    /// <summary>
    /// Represents a generic service for deleting entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class GenericDeleteService<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDeleteService{T}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager used for managing user entities.</param>
        public GenericDeleteService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Deletes an entity asynchronously based on the provided strategy.
        /// </summary>
        /// <param name="repository">The repository used for accessing entity data.</param>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <param name="hardDelete">A boolean value indicating whether to perform a hard delete or a soft delete.</param>
        /// <param name="context">The database context (optional) used for updating entities in the database.</param>
        /// <returns>A task representing the asynchronous operation, returning a message indicating the result of the deletion.</returns>
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, bool hardDelete, ExpenseReportingDbContext? context = null)
        {
            IDeleteStrategy<T> strategy = hardDelete ? new HardDeleteStrategy<T>(_userManager) : new SoftDeleteStrategy<T>(_userManager);
            return await strategy.DeleteAsync(repository, id, context);
        }
    }
}
