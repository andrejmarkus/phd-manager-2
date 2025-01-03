using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class SubjectRepository(ApplicationDbContext context, ILogger logger) : Repository<Subject>(context, logger), ISubjectRepository
    {
        public async Task<Subject?> GetByCodeAsync(string code)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(s => s.Code == code);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with code: {Code}", code);
                return null;
            }
        }
    }
}
