using PhDManager.Api.Services.Repositories;
using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class RegistrationRepository(ApplicationDbContext context, ILogger logger) : Repository<Registration>(context, logger), IRegistrationRepository
    {
    }
}
