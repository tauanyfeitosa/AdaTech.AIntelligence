using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    /// <summary>
    /// Service class for performing CRUD operations on expenses.
    /// </summary>
    public class ExpenseCRUDService : IExpenseCRUDService
    {
        private readonly IAIntelligenceRepository<Expense> _repository;
        private readonly GenericDeleteService<Expense> _deleteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseCRUDService"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing expense data.</param>
        /// <param name="deleteService">The service for deleting expenses.</param>
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
            expense.UpdatedAt = DateTime.Now;
            return await _repository.Update(expense);
        }

        /// <summary>
        /// Retrieves a single expense by its ID asynchronously.
        /// </summary>
        /// <param name="idExpense">The ID of the expense to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the expense if found and active; otherwise, throws a <see cref="NotFoundException"/>.</returns>
        public async Task<Expense?> GetOne(int idExpense)
        {
            var expense = await _repository.GetOne(idExpense);

            if (expense != null && expense.IsActive)
                return expense;

            throw new NotFoundException("Não existe despesa ativa com este ID.");
        }

        /// <summary>
        /// Retrieves all submitted expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of submitted and active expenses.</returns>
        public async Task<IEnumerable<Expense>?> GetAllSubmitted()
        {
            var allExpenses = await _repository.GetAll() ?? throw new NotFoundException("Não existem despesas submetidas.");
            return allExpenses.Where(expense => expense.Status == ExpenseStatus.SUBMITTED && expense.IsActive);
        }

        /// <summary>
        /// Retrieves all active expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of active expenses.</returns>
        public async Task<IEnumerable<Expense>?> GetAllActive()
        {
            var allExpenses = await _repository.GetAll() ?? throw new NotFoundException("Não existem despesas ativas.");
            return allExpenses.Where(expense => expense.IsActive);
        }

        /// <summary>
        /// Retrieves all expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all expenses.</returns>
        public Task<IEnumerable<Expense>?> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Retrieves all expenses from one particular user (actives and inactives) asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all expenses.</returns>
        public async Task<IEnumerable<Expense>?> GetUserExpenses(string userId)
        {
            var allExpenses = await _repository.GetAll();

            if(allExpenses == null)
                return null;

            var userExpenses = allExpenses.Where(expense => expense.UserInfoId == userId).ToList();

            return userExpenses;
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
