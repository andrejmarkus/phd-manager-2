using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public Repository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding entity.");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is not null)
                {
                    _dbSet.Remove(entity);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Entity with id: {Id} not found for deletion.", id);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting entity with id: {Id}", id);
                return false;
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(string id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with id: {Id}", id);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            try
            {
                var oldEntity = await _dbSet.FindAsync(id);
                if (oldEntity is not null)
                {
                    _context.Entry(oldEntity).CurrentValues.SetValues(entity);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Entity with id: {Id} not found for update.", id);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating entity with id: {Id}", id);
                return false;
            }
        }
    }
}
