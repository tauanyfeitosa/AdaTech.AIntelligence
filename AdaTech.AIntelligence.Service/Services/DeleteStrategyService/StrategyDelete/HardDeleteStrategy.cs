
using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete
{
    public class HardDeleteStrategy<T> : IDeleteStrategy<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        public HardDeleteStrategy(UserManager<UserInfo> userManager)
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
            var result = await _userManager.DeleteAsync(entityUser);

            if (result.Succeeded)
            {
                return "Deleção realizada com sucesso.";
            }

            return "Erro ao realizar a deleção.";
        }

        public async Task<string> DeleteEntityAsync(IAIntelligenceRepository<T> repository, object entity, ExpenseReportingDbContext? context = null)
        {
            if (context is not null)
            {
                context.Remove(entity);
            }
            else if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} não encontrado para exclusão. Experimente buscar por outro ID!");
            }

            try
            {
                var success = await repository.Delete((T)entity);

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
