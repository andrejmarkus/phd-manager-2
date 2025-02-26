using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetCurrentByUserId(string? userId);
    }
}
