using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class HardDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, ILogger logger, DbContext context, int id)
        {
            var entity = await repository.GetOne(id);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão. Experimente buscar por outro ID!");
            }

            try
            {
                var success = await repository.Delete(entity);

                if (!success)
                {
                    throw new InvalidOperationException("Falha ao realizar hard delete. Tente novamente!");
                }

                return "Excluido com sucesso!";

            }
            catch (Exception)
            {
                throw new InvalidOperationException("Falha ao realizar hard delete. Tente novamente! Verifique objetos relacionados ou considere um soft delete.");
            }
        }
    }
}
