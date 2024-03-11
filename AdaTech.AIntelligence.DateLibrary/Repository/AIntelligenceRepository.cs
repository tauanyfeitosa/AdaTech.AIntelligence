using AdaTech.AIntelligence.DateLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace AdaTech.AIntelligence.DateLibrary.Repository
{
    public class AIntelligenceRepository<T> : IAIntelligenceRepository<T> where T : class
    {
        private readonly ExpenseReportingDbContext _context;

        public AIntelligenceRepository(ExpenseReportingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetOne(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
