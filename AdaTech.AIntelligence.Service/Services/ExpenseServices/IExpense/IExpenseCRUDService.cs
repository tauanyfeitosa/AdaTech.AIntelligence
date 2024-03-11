using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    public interface IExpenseCRUDService
    {
        Task<bool> CreateExpense(string response);
    }
}
