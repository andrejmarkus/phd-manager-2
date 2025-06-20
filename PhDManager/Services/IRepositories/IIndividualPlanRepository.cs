﻿using PhDManager.Models;

namespace PhDManager.Services.IRepositories
{
    public interface IIndividualPlanRepository : IRepository<IndividualPlan>
    {
        Task<IndividualPlan?> GetByUserIdAsync(string userId);
        Task<IndividualPlan?> GetByGuidAsync(string guid);
    }
}
