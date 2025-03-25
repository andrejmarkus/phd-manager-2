using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models.Enums;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class StudentRepository(ApplicationDbContext context, ILogger logger) : Repository<Student>(context, logger), IStudentRepository
    {
        public async Task<Student?> GetByUserIdAsync(string? userId)
        {
            if (userId is null) return null;

            try
            {
                return await _dbSet.FirstOrDefaultAsync(s => s.User.Id == userId && s.State == StudentState.Study);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with id: {Id}", userId);
                return null;
            }
        }
    }
}
