using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class InvitationRepository(ApplicationDbContext context, ILogger logger) : Repository<Invitation>(context, logger), IInvitationRepository
    {
        public async Task<Invitation?> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(i => i.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting entity with email: {Email}", email);
                return null;
            }
        }

        public async Task<Invitation?> GetByGuidAsync(string guid)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(i => i.Guid == guid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting entity with guid: {Guid}", guid);
                return null;
            }
        }
    }
}
