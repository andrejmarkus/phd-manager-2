using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class SystemStateRepository(ApplicationDbContext context, ILogger logger) : Repository<SystemState>(context, logger), ISystemStateRepository
    {
        public async Task<SystemState?> GetAsync()
        {
            try
            {
                return await _dbSet.FirstAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting system state");
                return null;
            }
        }

        public async Task<bool> SetAsync(SystemState systemState)
        {
            var oldSystemState = await GetAsync();
            if (oldSystemState is null)
            {
                try
                {
                    await AddAsync(systemState);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while setting system state");
                    return false;
                }
            }
            else
            {
                try
                {
                    _context.Entry(oldSystemState).CurrentValues.SetValues(systemState);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while setting system state");
                    return false;
                }
            }
        }
    }
}
