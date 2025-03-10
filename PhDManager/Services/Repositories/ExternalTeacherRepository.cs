using PhDManager.Data;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class ExternalTeacherRepository(ApplicationDbContext context, ILogger logger) : Repository<ExternalTeacher>(context, logger), IExternalTeacherRepository
    {
    }
}
