using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class TeacherRepository(ApplicationDbContext context, ILogger logger) : Repository<Teacher>(context, logger), ITeacherRepository
    {
        public async Task<Teacher?> GetByDisplayNameAsync(string displayName)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(t => t.User.DisplayName != null && EF.Functions.Like(t.User.DisplayName, $"%{displayName}%"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting entity with dispaly name: {DisplayName}", displayName);
                return null;
            }
        }
    }
}
