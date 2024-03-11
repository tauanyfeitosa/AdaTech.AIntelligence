namespace AdaTech.AIntelligence.DateLibrary.Repository
{
    public interface IAIntelligenceRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> GetAll();  
        Task<T> GetOne (int id);
    }
}
