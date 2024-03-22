using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense
{
    /// <summary>
    /// Interface for expense CRUD operations.
    /// </summary>
    public interface IExpenseCRUDService
    {
        /// <summary>
        /// Creates a new expense asynchronously.
        /// </summary>
        /// <param name="expense">The expense object to create.</param>
        /// <returns>A task representing the asynchronous operation, returning a boolean indicating whether the operation was successful.</returns>
        Task<bool> CreateExpense(Expense expense);

        /// <summary>
        /// Updates an existing expense asynchronously.
        /// </summary>
        /// <param name="expense">The expense object to update.</param>
        /// <returns>A task representing the asynchronous operation, returning a boolean indicating whether the operation was successful.</returns>
        Task<bool> UpdateExpense(Expense expense);

        /// <summary>
        /// Retrieves a single expense asynchronously by its ID.
        /// </summary>
        /// <param name="idExpense">The ID of the expense to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the expense object if found.</returns>
        Task<Expense?> GetOne(int idExpense);

        /// <summary>
        /// Retrieves all submitted expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of submitted expenses.</returns>
        Task<IEnumerable<Expense>?> GetAllSubmitted();

        /// <summary>
        /// Retrieves all active expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of active expenses.</returns>
        Task<IEnumerable<Expense>?> GetAllActive();

        /// <summary>
        /// Retrieves all expenses asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of all expenses.</returns>
        Task<IEnumerable<Expense>?> GetAll();

        /// <summary>
        /// Deletes an expense asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <param name="isHardDelete">A boolean value indicating whether to perform a hard delete or a soft delete.</param>
        /// <returns>A task representing the asynchronous operation, returning a message indicating the result of the deletion.</returns>
        Task<string> DeleteAsync(int id, bool isHardDelete);
    }
}
