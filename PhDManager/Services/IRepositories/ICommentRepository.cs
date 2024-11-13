using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>?> GetAllByThesisAsync(int thesisId);
    }
}
