using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IStudyProgramRepository : IRepository<StudyProgram>
    {
        Task<StudyProgram?> GetByCodeAsync(string code);
    }
}
