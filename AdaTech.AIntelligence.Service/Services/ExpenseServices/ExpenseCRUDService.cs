
using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public class ExpenseCRUDService : IExpenseCRUDService
    {
        private readonly IAIntelligenceRepository<Expense> _repository;

        public ExpenseCRUDService(IAIntelligenceRepository<Expense> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateExpense(string response)
        {
            try 
            {
                string[] valores = response.Split(",");
                var respostaObjeto = new Expense()
                {
                    Category = (Category)int.Parse(valores[0]),
                    TotalValue = double.Parse(valores[1].Replace(".", ",")),
                    Description = valores[2],
                    Status = ExpenseStatus.SUBMETIDO,
                };

                var success = await _repository.Create(respostaObjeto);

                return success;

            } catch
            {
                throw new Exception($"{response} \nVerifique possíveis problemas com a resolução da imagem enviada!");
            }

        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            return await _repository.Update(expense);
        }

        public async Task<Expense> GetOne(int idExpense)
        {
            return await _repository.GetOne(idExpense);
        }

        public async Task<IEnumerable<Expense>> GetAllSubmetido()
        {
            var allExpenses = await _repository.GetAll();
            return allExpenses.Where(expense => expense.Status == ExpenseStatus.SUBMETIDO && expense.IsActive);
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
    }
}
