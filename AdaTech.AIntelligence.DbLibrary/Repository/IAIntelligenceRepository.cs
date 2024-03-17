namespace AdaTech.AIntelligence.DbLibrary.Repository
{
    /// <summary>
    /// Interface for the AIntelligenceRepository.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IAIntelligenceRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the creation is successful; otherwise, false.</returns>
        Task<bool> Create(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update is successful; otherwise, false.</returns>
        Task<bool> Update(T entity);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the deletion is successful; otherwise, false.</returns>
        Task<bool> Delete(T entity);

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all entities.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Retrieves a single entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the entity if found; otherwise, null.</returns>
        Task<T> GetOne(int id);
    }
}
