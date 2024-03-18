using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    /// <summary>
    /// Represents the hard delete strategy for deleting entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class HardDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HardDeleteStrategy{T}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public HardDeleteStrategy(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Deletes asynchronously using the provided repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <param name="context">The database context (optional).</param>
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, ExpenseReportingDbContext? context = null)
        {
            IdentityResult result = null;

            if (int.TryParse(id, out int intId))
            {
                var entity = await repository.GetOne(intId);
                return await DeleteEntityAsync(repository, entity, context);
            }

            var entityUser = await _userManager.FindByIdAsync(id);

            if (entityUser is not null)
            {
                result = await _userManager.DeleteAsync(entityUser);
            }

            if (result is not null && result.Succeeded)
            {
                return "Deleção realizada com sucesso.";
            }

            throw new UnprocessableEntityException("Falha ao deletar usuário. Tente novamente!");
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="context">The database context (optional).</param>
        public async Task<string> DeleteEntityAsync(IAIntelligenceRepository<T> repository, object entity, ExpenseReportingDbContext? context = null)
        {
            if (context is not null)
            {
                context.Remove(entity);
            }
            else if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão.");
            }

            try
            {
                var success = await repository.Delete((T)entity);

                if (!success)
                {
                    throw new UnprocessableEntityException("Falha ao realizar hard delete. Tente novamente!");
                }

                return "Excluido com sucesso!";

            }
            catch (Exception)
            {
                throw new InvalidOperationException("Falha ao realizar Hard Delete.");
            }
        }
    }
}
