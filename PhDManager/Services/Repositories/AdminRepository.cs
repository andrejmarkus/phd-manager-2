using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class AdminRepository(ApplicationDbContext context, ILogger logger) : Repository<Admin>(context, logger), IAdminRepository
    {
    }
}
