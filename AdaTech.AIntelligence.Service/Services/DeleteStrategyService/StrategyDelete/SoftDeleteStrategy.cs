using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    public class SoftDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, int id, ExpenseReportingDbContext? context = null)
        {
            var entity = await repository.GetOne(id);

            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão. Experimente buscar por outro ID!");

            var propertyInfo = entity.GetType().GetProperty("IsActive");

            if (propertyInfo == null || !propertyInfo.CanWrite || !(bool)propertyInfo.GetValue(entity))
                throw new InvalidOperationException("A propriedade 'IsActive' não foi encontrada ou não pode ser escrita. Falha na operação.");

            propertyInfo.SetValue(entity, false, null);

            bool success;

            if (context is not null)
            {
                var updatedSuccessfully = context.Update(entity);
                success = updatedSuccessfully is not null;
            }
            else
                success = await repository.Update(entity);

            if (!success)
                throw new InvalidOperationException($"Falha ao marcar a entidade como inativa. Tente novamente!");

            return "Marcado como inativo com sucesso!";
        }
    }
}