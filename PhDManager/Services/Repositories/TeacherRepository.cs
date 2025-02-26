using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class TeacherRepository(ApplicationDbContext context, ILogger logger) : Repository<Teacher>(context, logger), ITeacherRepository
    {
    }
}
