using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface ISubjectRepository: IRepository<Subject>
    {
        Task<Subject?> GetByCodeAsync(string code);
    }
}
