using PhDManager.Models.Roles;

namespace PhDManager.Services.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByUserIdAsync(string? userId);
    }
}
