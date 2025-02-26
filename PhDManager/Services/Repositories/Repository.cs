using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
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

        public bool Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting entity with id: {Id}", entity.Id);
                return false;
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
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

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var oldEntity = await _dbSet.FindAsync(entity.Id);
                if (oldEntity is not null)
                {
                    _context.Entry(oldEntity).CurrentValues.SetValues(entity);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Entity with id: {Id} not found for update.", entity.Id);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating entity with id: {Id}", entity.Id);
                return false;
            }
        }
    }
}
