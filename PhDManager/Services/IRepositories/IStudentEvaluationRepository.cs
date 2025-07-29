using PhDManager.Models.Documents;

namespace PhDManager.Services.IRepositories
{
    public interface IStudentEvaluationRepository : IRepository<StudentEvaluation>
    {
        Task<StudentEvaluation?> GetByGuidAsync(string guid);
    }
}
