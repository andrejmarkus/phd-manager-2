using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class ThesisRepository(ApplicationDbContext context, ILogger logger) : Repository<Thesis>(context, logger), IThesisRepository
    {
    }
}
