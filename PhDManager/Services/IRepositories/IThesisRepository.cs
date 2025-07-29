using PhDManager.Models;
using PhDManager.Models.Documents;

namespace PhDManager.Services.IRepositories
{
    public interface IThesisRepository : IRepository<Thesis>
    {
        Task<Thesis?> GetByGuidAsync(string guid);
        Task<IEnumerable<string>?> GetAllSchoolYearsAsync();
    }
}
