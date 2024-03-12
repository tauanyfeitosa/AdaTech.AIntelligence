using AdaTech.AIntelligence.DateLibrary.Context;
using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.DeleteStrategyService
{
    public class DeleteService
    {
        private readonly ExpenseReportingDbContext _context;
        private readonly IAIntelligenceRepository<Expense> _repository;
        private IDeleteStrategy<Expense> _deleteStrategy { get; set; }

        public DeleteService(IAIntelligenceRepository<Expense> repository, ExpenseReportingDbContext context, IDeleteStrategy<Expense> deleteStrategy)
        {
            _repository = repository;
            _context = context;
            _deleteStrategy = deleteStrategy;
        }

        public async Task<string> DeleteAsync(int id, bool isHardDelete)
        {
            if (isHardDelete)
                _deleteStrategy = new HardDeleteStrategy<Expense>();
            else
                _deleteStrategy = new SoftDeleteStrategy<Expense>();


            string result = await _deleteStrategy.DeleteAsync(_repository, id, _context);

            return result;
        }
    }
}
