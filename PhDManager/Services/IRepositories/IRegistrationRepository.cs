using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IRegistrationRepository: IRepository<Registration>
    {
        Task<Registration?> GetByGuidAsync(string guid);
    }
}
