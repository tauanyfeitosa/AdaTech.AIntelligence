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
        private IDeleteStrategy<Expense> _deleteStrategy { get; set; }

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
                    Status = ExpenseStatus.SUBMITTED,
                    IsActive = true
                };

                var success = await _repository.Create(respostaObjeto);

                return success;

            }
            catch
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
            if (isHardDelete)
                _deleteStrategy = new HardDeleteStrategy<Expense>();
            else
                _deleteStrategy = new SoftDeleteStrategy<Expense>();


            string result = await _deleteStrategy.DeleteAsync(_repository, id);

            return result;
        }
    }
}
