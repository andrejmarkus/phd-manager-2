using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IExamSupervisorRepository : IRepository<ExamSupervisor>
    {
        Task<ExamSupervisor?> GetByGuidAsync(string guid);
    }
}
