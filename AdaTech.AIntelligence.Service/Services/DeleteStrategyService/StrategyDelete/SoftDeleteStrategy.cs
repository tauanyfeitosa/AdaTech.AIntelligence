using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    public class SoftDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        public SoftDeleteStrategy(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

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