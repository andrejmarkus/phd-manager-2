using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class ThesisRepository(ApplicationDbContext context, ILogger logger) : Repository<Thesis>(context, logger), IThesisRepository
    {
        public async Task<Thesis?> GetByGuidAsync(string guid)
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

        public async Task<IEnumerable<string>?> GetAllSchoolYearsAsync()
        {
            try
            {
                return await _dbSet.Where(t => t.SchoolYears != null).SelectMany(t => t.SchoolYears).Distinct().ToArrayAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting school years");
                return null;
            }
        }
    }
}
