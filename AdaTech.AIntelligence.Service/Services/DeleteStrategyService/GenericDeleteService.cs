
using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class GenericDeleteService<T> where T : class
    {
        private readonly UserManager<UserInfo> _userManager;

        public GenericDeleteService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> DeleteAsync(IAIntelligenceRepository<T> repository, string id, bool hardDelete, ExpenseReportingDbContext? context = null)
        {
            IDeleteStrategy<T> strategy = hardDelete ? new HardDeleteStrategy<T>(_userManager) : new SoftDeleteStrategy<T>(_userManager);
            return await strategy.DeleteAsync(repository, id, context);
        }
    }
}
