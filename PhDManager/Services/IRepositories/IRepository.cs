namespace PhDManager.Services.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
    }
}
