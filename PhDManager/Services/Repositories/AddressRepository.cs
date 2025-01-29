using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class AddressRepository(ApplicationDbContext context, ILogger logger) : Repository<Address>(context, logger), IAddressRepository
    {
    }
}
