using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public class ExpenseCRUDService : IExpenseCRUDService
    {
        private readonly IAIntelligenceRepository<Expense> _repository;
        private readonly GenericDeleteService<Expense> _deleteService;

        public ExpenseCRUDService(IAIntelligenceRepository<Expense> repository, GenericDeleteService<Expense> deleteService)
        {
            _repository = repository;
            _deleteService = deleteService;
        }

        public async Task<bool> CreateExpense(Expense expense)
        {
            var success = await _repository.Create(expense);

            return success;
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            return await _repository.Update(expense);
        }

        public async Task<Expense> GetOne(int idExpense)
        {
            var expense = await _repository.GetOne(idExpense);

            if (expense != null && expense.IsActive)
                return expense;

            throw new NotFoundException("Não foi localizada uma nota ativa com o ID fornecido. Tente novamente.");
        }

        public async Task<IEnumerable<Expense>> GetAllSubmitted()
        {
            var allExpenses = await _repository.GetAll();
            return allExpenses.Where(expense => expense.Status == ExpenseStatus.SUBMITTED && expense.IsActive);
        }

        public async Task<IEnumerable<Expense>> GetAllActive()
        {
            var allExpenses = await _repository.GetAll();
            return allExpenses.Where(expense => expense.IsActive);
        }

        public Task<IEnumerable<Expense>> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<string> DeleteAsync(int id, bool isHardDelete)
        {
            return await _deleteService.DeleteAsync(_repository, id, isHardDelete);
        }
    }
}
