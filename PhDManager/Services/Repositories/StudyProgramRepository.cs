using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class StudyProgramRepository(ApplicationDbContext context, ILogger logger) : Repository<StudyProgram>(context, logger), IStudyProgramRepository
    {
        public async Task<StudyProgram?> GetByCodeAsync(string code)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(sp => sp.Code == code);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with code: {Code}", code);
                return null;
            }
        }
    }
}
