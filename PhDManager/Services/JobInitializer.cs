using Hangfire;

namespace PhDManager.Services
{
    public class JobInitializer(UsersService usersService)
    {
        public void Initialize()
        {
            RecurringJob.AddOrUpdate("updateLdapUser", () => usersService.UpdateUsers(), Cron.Daily);
            RecurringJob.AddOrUpdate("clearRegistrations", () => usersService.ClearRegistrations(), Cron.Daily);
        }
    }
}
