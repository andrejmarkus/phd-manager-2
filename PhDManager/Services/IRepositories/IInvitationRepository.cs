using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Task<Invitation?> GetByGuidAsync(string guid);
        Task<Invitation?> GetByEmailAsync(string email);
    }
}
