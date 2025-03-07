using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IStudentEvaluationRepository : IRepository<StudentEvaluation>
    {
        Task<StudentEvaluation?> GetByGuidAsync(string guid);
    }
}
