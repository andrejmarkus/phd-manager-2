using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class ExternalRepository(ApplicationDbContext context, ILogger logger) : Repository<External>(context, logger), IExternalRepository
    {
    }
}
