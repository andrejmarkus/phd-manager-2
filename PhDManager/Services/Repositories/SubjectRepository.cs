using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class SubjectRepository(ApplicationDbContext context, ILogger logger) : Repository<Subject>(context, logger), ISubjectRepository
    {
    }
}
