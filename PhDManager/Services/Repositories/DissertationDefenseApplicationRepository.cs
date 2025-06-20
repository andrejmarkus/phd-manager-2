﻿using PhDManager.Data;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class DissertationDefenseApplicationRepository(ApplicationDbContext context, ILogger logger) : Repository<DissertationDefenseApplication>(context, logger), IDissertationDefenseApplicationRepository
    {
    }
}
