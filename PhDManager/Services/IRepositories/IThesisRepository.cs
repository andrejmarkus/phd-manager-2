using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IThesisRepository: IRepository<Thesis>
    {
        Task<Thesis?> GetByGuidAsync(string guid);
    }
}
