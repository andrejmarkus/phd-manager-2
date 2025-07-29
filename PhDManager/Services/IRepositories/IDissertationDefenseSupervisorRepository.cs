using PhDManager.Models.Documents;

namespace PhDManager.Services.IRepositories
{
    public interface IDissertationDefenseSupervisorRepository : IRepository<DissertationDefenseSupervisor>
    {
        Task<DissertationDefenseSupervisor?> GetByGuidAsync(string guid);
    }
}
