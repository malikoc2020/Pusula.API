namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsQueryable();
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task<bool> SaveChangesAsync();
    }
}
