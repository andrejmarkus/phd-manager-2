using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class CommentRepository(ApplicationDbContext context, ILogger logger) : Repository<Comment>(context, logger), ICommentRepository
    {
        public async Task<IEnumerable<Comment>?> GetAllByThesisAsync(int thesisId)
        {
            try
            {
                return await _dbSet.Where(c => c.ThesisId == thesisId).ToArrayAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting entity with thesis id: {Id}", thesisId);
                return null;
            }
        }
    }
}
