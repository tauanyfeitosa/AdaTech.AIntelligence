
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public class ExpenseCRUDService : IExpenseCRUDService
    {
        public async Task<Expense> CreateExpense(string response)
        {
            string[] valores = response.Split(",");
            var respostaObjeto = new Expense()
            {
                Category = (Category)int.Parse(valores[0]),
                TotalValue = double.Parse(valores[1]),
                Description = valores[1],
                Status = ExpenseStatus.SUBMETIDO,
            };

            return respostaObjeto;
        }
    }
}
