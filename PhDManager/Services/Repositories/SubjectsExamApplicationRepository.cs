using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class SubjectsExamApplicationRepository(ApplicationDbContext context, ILogger logger) : Repository<SubjectsExamApplication>(context, logger), ISubjectsExamApplicationRepository
    {
    }
}
