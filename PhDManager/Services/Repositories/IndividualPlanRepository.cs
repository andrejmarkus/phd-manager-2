using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models.Documents;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class IndividualPlanRepository(ApplicationDbContext context, ILogger logger) : Repository<IndividualPlan>(context, logger), IIndividualPlanRepository
    {
        public async Task<IndividualPlan?> GetByUserIdAsync(string userId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(ip => ip.Student != null && ip.Student.User.Id == userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with id: {Id}", userId);
                return null;
            }
        }

        public async Task<IndividualPlan?> GetByGuidAsync(string guid)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(t => t.Guid == guid);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with guid: {Guid}", guid);
                return null;
            }
        }
    }
}
