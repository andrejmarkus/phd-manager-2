using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class StudentRepository(ApplicationDbContext context, ILogger logger, SchoolYearService schoolYearService) : Repository<Student>(context, logger), IStudentRepository
    {
        public async Task<Student?> GetCurrentByUserId(string? userId)
        {
            if (userId is null) return null;

            try
            {
                return await _dbSet.FirstOrDefaultAsync(ds => ds.User.Id == userId && ds.StartSchoolYear == schoolYearService.CurrentSchoolYear);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with id: {Id}", userId);
                return null;
            }
        }
    }
}
