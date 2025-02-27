using Hangfire;

namespace PhDManager.Services
{
    public class JobInitializer(UsersService usersService, AppStateService appStateService)
    {
        public void Initialize()
        {
            RecurringJob.AddOrUpdate("updateLdapUser", () => usersService.UpdateUsers(), Cron.Daily);
            RecurringJob.AddOrUpdate("clearRegistrations", () => usersService.ClearRegistrations(), Cron.Daily);
            RecurringJob.AddOrUpdate("openSystem", () => appStateService.SetSystemOpenAsync(true), "0 0 1 9 *");
            RecurringJob.AddOrUpdate("closeSystem", () => appStateService.SetSystemOpenAsync(false), "0 0 1 1 *");
        }
    }
}
