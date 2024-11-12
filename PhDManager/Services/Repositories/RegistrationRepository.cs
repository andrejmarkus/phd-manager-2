using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class RegistrationRepository(ApplicationDbContext context, ILogger logger) : Repository<Registration>(context, logger), IRegistrationRepository
    {
        public async Task<Registration?> GetByGuidAsync(string guid)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(r => r.Guid == guid);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with guid: {Guid}", guid);
                return null;
            }
        }
    }
}
