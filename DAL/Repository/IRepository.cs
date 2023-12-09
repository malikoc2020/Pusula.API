namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsQueryable();
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> SaveChangesAsync();
    }
}
