using AdaTech.AIntelligence.DbLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace AdaTech.AIntelligence.DbLibrary.Repository
{
    /// <summary>
    /// Repository generic class for the application.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class AIntelligenceRepository<T> : IAIntelligenceRepository<T> where T : class
    {
        private readonly ExpenseReportingDbContext _context;

        public AIntelligenceRepository(ExpenseReportingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the creation is successful; otherwise, false.</returns>
        public async Task<bool> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the deletion is successful; otherwise, false.</returns>
        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all entities.</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Retrieves a single entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the entity if found; otherwise, null.</returns>
        public async Task<T> GetOne(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update is successful; otherwise, false.</returns>
        public async Task<bool> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
