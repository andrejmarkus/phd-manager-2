using Hangfire;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services
{
    public class JobService(UsersService UsersService, IUnitOfWork UnitOfWork)
    {
        public string UpdateLdapUserId { get; } = "updateLdapUser";
        public string ClearRegistrationsId { get; } = "clearRegistrations";
        public string OpenSystemId { get; } = "openSystem";
        public string CloseSystemId { get; } = "closeSystem";

        public void Initialize()
        {
            RecurringJob.AddOrUpdate(UpdateLdapUserId, () => UsersService.UpdateUsers(), Cron.Daily);
            RecurringJob.AddOrUpdate(ClearRegistrationsId, () => UsersService.ClearRegistrations(), Cron.Daily);
        }

        public async Task SetSystemOpenAsync(bool open)
        {
            var systemState = await UnitOfWork.SystemState.GetAsync();
            if (systemState is null)
            {
                systemState = new SystemState { IsOpen = open };
                await UnitOfWork.SystemState.SetAsync(systemState);
                await UnitOfWork.CompleteAsync();
            }
            else
            {
                systemState.IsOpen = open;
                await UnitOfWork.SystemState.SetAsync(systemState);
                await UnitOfWork.CompleteAsync();
            }
        }
    }
}
