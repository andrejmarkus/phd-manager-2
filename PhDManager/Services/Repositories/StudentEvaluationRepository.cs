using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models.Documents;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class StudentEvaluationRepository(ApplicationDbContext context, ILogger logger) : Repository<StudentEvaluation>(context, logger), IStudentEvaluationRepository
    {
        public async Task<StudentEvaluation?> GetByGuidAsync(string guid)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(se => se.Guid == guid);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with guid: {Guid}", guid);
                return null;
            }
        }
    }
}
