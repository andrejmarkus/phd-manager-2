using PhDManager.Models.Roles;

namespace PhDManager.Services.IRepositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher?> GetByDisplayNameAsync(string displayName);
    }
}
