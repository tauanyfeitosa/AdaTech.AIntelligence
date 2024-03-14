using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    public interface IExpenseCRUDService
    {
        Task<bool> CreateExpense(Expense expense);
        Task<bool> UpdateExpense(Expense expense);
        Task<Expense> GetOne(int idExpense);
        Task<IEnumerable<Expense>> GetAllSubmitted();
        Task<IEnumerable<Expense>> GetAllActive();
        Task<IEnumerable<Expense>> GetAll();
        Task<string> DeleteAsync(int id, bool isHardDelete);
    }
}
