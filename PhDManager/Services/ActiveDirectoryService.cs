using LdapForNet;
using Microsoft.Extensions.Options;
using PhDManager.Models.Options;
using static LdapForNet.Native.Native;

namespace PhDManager.Services
{
    public class ActiveDirectoryService(IOptions<ActiveDirectoryOptions> options)
    {
        private readonly ActiveDirectoryOptions _options = options.Value;

        public async Task<LdapEntry?> AuthenticateUserAsync(string username, string password)
        {
            IEnumerable<LdapEntry> entries;

            try
            {
                var connection = await CreateConnection(username, password);
                entries = await connection.SearchAsync(_options.LdapDomain, $"(uid={username})");
            }
            catch
            {
                return null;
            }

            var entry = entries.FirstOrDefault();
            return entry ?? null;
        }

        public async Task<IEnumerable<LdapEntry>?> SearchUsersAsync(string username)
        {
            IEnumerable<LdapEntry> entries;

            try
            {
                var connection = await CreateConnection(_options.Username, _options.Password);
                entries = await connection.SearchAsync(_options.LdapDomain, $"(uid={username}*)");
            }
            catch
            {
                return null;
            }

            return entries;
        }

        public async Task<LdapEntry?> SearchUserAsync(string username)
        {
            LdapEntry? entry;

            try
            {
                var connection = await CreateConnection(_options.Username, _options.Password);
                entry = (await connection.SearchAsync(_options.LdapDomain, $"(uid={username}*)")).FirstOrDefault();
            }
            catch
            {
                return null;
            }

            return entry;
        }

        public async Task<LdapEntry?> SearchUserByDisplayNameAsync(string displayName)
        {
            LdapEntry? entry;

            try
            {
                var connection = await CreateConnection(_options.Username, _options.Password);
                entry = (await connection.SearchAsync(_options.LdapDomain, $"(&(displayName=*{displayName}*)(mail=*))")).FirstOrDefault();
            }
            catch
            {
                return null;
            }

            return entry;
        }

        private async Task<LdapConnection> CreateConnection(string username, string password)
        {
            var connection = new LdapConnection();
            connection.Connect(_options.LdapPath, 389, LdapSchema.LDAP);
            connection.SetOption(LdapOption.LDAP_OPT_REFERRALS, nint.Zero);
            await connection.BindAsync(LdapAuthType.Simple, new LdapCredential
            {
                UserName = $"{username}@{_options.LdapPath}",
                Password = password
            });

            return connection;
        }
    }
}
