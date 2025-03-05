using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class IndividualPlanSubjectRepository(ApplicationDbContext context, ILogger logger) : Repository<IndividualPlanSubject>(context, logger), IIndividualPlanSubjectRepository
    {
    }
}
