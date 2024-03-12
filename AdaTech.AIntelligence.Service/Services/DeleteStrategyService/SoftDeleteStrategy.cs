using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class SoftDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, ILogger logger, DbContext context, int id)
        {
            var entity = await repository.GetOne(id);
            if(entity == null)
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão. Experimente buscar por outro ID!");

            var propertyInfo = entity.GetType().GetProperty("IsActive");

            if (propertyInfo == null || propertyInfo.CanWrite)
                throw new InvalidOperationException("A propriedade 'IsActive' não foi encontrada ou não pode ser escrita. Falha na operação");
                        
            propertyInfo.SetValue(entity, false, null);

            var success = await repository.Update(entity);

            if (!success)
                throw new InvalidOperationException($"Falha ao marcar a entidade como inativa. Tente novamente!");

            return "Marcado como inativo com sucesso!";
        }
    }
}