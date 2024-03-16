using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.DataLibrary.Repository;

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

        /// <summary>
        /// Creates a new expense asynchronously.
        /// </summary>
        /// <param name="expense">The expense to create.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the creation is successful; otherwise, false.</returns>
        public async Task<bool> CreateExpense(Expense expense)
        {
            var success = await _repository.Create(expense);

            return success;
        }

        /// <summary>
        /// Updates an existing expense asynchronously.
        /// </summary>
        /// <param name="expense">The expense to update.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update is successful; otherwise, false.</returns>
        public async Task<bool> UpdateExpense(Expense expense)
        {
            return await _repository.Update(expense);
        }

        /// <summary>
        /// Retrieves a single expense by its ID asynchronously.
        /// </summary>
        /// <param name="idExpense">The ID of the expense to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the expense if found and active; otherwise, throws a <see cref="NotFoundException"/>.</returns>
        public async Task<Expense> GetOne(int idExpense)
        {
            var expense = await _repository.GetOne(idExpense);

            if (expense != null && expense.IsActive)
                return expense;

            throw new NotFoundException("Não foi localizada uma nota ativa com o ID fornecido. Tente novamente.");
        }

        /// <summary>
        /// Retrieves all submitted expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of submitted and active expenses.</returns>
        public async Task<IEnumerable<Expense>> GetAllSubmitted()
        {
            var allExpenses = await _repository.GetAll();
            return allExpenses.Where(expense => expense.Status == ExpenseStatus.SUBMITTED && expense.IsActive);
        }

        /// <summary>
        /// Retrieves all active expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of active expenses.</returns>
        public async Task<IEnumerable<Expense>> GetAllActive()
        {
            var allExpenses = await _repository.GetAll();
            return allExpenses.Where(expense => expense.IsActive);
        }

        /// <summary>
        /// Retrieves all expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all expenses.</returns>
        public Task<IEnumerable<Expense>> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Deletes an expense asynchronously.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <param name="isHardDelete">A boolean indicating whether to perform a hard delete or a soft delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the delete operation.</returns>
        public async Task<string> DeleteAsync(int id, bool isHardDelete)
        {
            return await _deleteService.DeleteAsync(_repository, id.ToString(), isHardDelete);
        }
    }
}
