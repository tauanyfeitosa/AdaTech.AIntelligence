using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    public interface IExpenseCRUDService
    {
        Task<Expense> CreateExpense(string response);
    }
}
