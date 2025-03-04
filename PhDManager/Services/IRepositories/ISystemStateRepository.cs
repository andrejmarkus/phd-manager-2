using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface ISystemStateRepository : IRepository<SystemState>
    {
        Task<SystemState?> GetAsync();
        Task<bool> SetAsync(SystemState systemState);
    }
}
