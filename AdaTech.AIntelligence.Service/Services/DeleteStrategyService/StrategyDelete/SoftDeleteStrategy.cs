using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    /// <summary>
    /// Represents the soft delete strategy for deleting entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class SoftDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteStrategy{T}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager used for managing user entities.</param>
        public SoftDeleteStrategy(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="repository">The repository used for accessing entity data.</param>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <param name="context">The database context (optional) used for updating entities in the database.</param>
        /// <returns>A task representing the asynchronous operation, returning a message indicating the result of the deletion.</returns>
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, ExpenseReportingDbContext? context = null)
        {
            if (int.TryParse(id, out int intId))
            {
                var entity = await repository.GetOne(intId);
                return await DeleteEntityAsync(repository, entity, context);
            }

            var entityUser = await _userManager.FindByIdAsync(id);
            return await DeleteEntityAsync(repository, entityUser, context);
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="repository">The repository used for accessing entity data.</param>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="context">The database context (optional) used for updating entities in the database.</param>
        /// <returns>A task representing the asynchronous operation, returning a message indicating the result of the deletion.</returns>
        public async Task<string> DeleteEntityAsync(IAIntelligenceRepository<T> repository, object entity, ExpenseReportingDbContext? context = null)
        {
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão. Experimente buscar por outro ID!");

            var propertyInfo = entity.GetType().GetProperty("IsActive");

            if (propertyInfo == null || !propertyInfo.CanWrite || !(bool)propertyInfo.GetValue(entity))
                throw new InvalidOperationException("A propriedade 'IsActive' não foi encontrada ou não pode ser escrita. Falha na operação.");

            propertyInfo.SetValue(entity, false, null);

            bool success;

            if (context is not null)
            {
                var updatedSuccessfully = context.Update((T)entity);
                success = updatedSuccessfully is not null;
            }
            else
                success = await repository.Update((T)entity);

            if (!success)
                throw new InvalidOperationException($"Falha ao marcar a entidade como inativa. Tente novamente!");

            return "Marcado como inativo com sucesso!";
        }
    }
}