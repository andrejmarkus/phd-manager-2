using PhDManager.Data;
using PhDManager.IRepositories;
using PhDManager.Models;

namespace PhDManager.Api.Services.Repositories
{
    public class ThesisRepository(ApplicationDbContext context, ILogger logger) : Repository<Thesis>(context, logger), IThesisRepository
    {
    }
}
