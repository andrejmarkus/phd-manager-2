using Hangfire;
using PhDManager.Models;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services
{
    public class JobService(UsersService usersService, IUnitOfWork unitOfWork)
    {
        public string UpdateLdapUserId => "updateLdapUser";
        public string OpenSystemId => "openSystem";
        public string CloseSystemId => "closeSystem";

        public void Initialize()
        {
            RecurringJob.AddOrUpdate(UpdateLdapUserId, () => usersService.UpdateUsers(), Cron.Daily);
        }

        public async Task SetSystemOpenAsync(bool open)
        {
            var systemState = await unitOfWork.SystemState.GetAsync();
            if (systemState is null)
            {
                systemState = new SystemState { IsOpen = open };
            }
            else
            {
                systemState.IsOpen = open;
            }

            await unitOfWork.SystemState.SetAsync(systemState);
            await unitOfWork.CompleteAsync();
        }
    }
}
