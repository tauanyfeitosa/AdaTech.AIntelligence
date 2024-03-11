using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    public interface IExpenseCRUDService
    {
        Task<bool> CreateExpense(string response);
        Task<bool> UpdateExpense(Expense expense);
        Task<Expense> GetOne(int idExpense);
        Task<IEnumerable<Expense>> GetAllSubmetido();
        Task<IEnumerable<Expense>> GetAllActive();
        Task<IEnumerable<Expense>> GetAll();
    }
}
