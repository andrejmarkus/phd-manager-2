using PhDManager.Data;
using PhDManager.Models.Documents;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class ExamApplicationRepository(ApplicationDbContext context, ILogger logger) : Repository<ExamApplication>(context, logger), IExamApplicationRepository
    {
    }
}
