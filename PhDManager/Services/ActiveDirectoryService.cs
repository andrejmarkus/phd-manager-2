using LdapForNet;
using Microsoft.Extensions.Options;
using PhDManager.Core.Models;
using static LdapForNet.Native.Native;

namespace PhDManager.Services
{
    public class ActiveDirectoryService(IOptions<ActiveDirectoryOptions> options)
    {
        ActiveDirectoryOptions _options = options.Value;

        public async Task<LdapEntry?> AuthenticateUserAsync(string username, string password)
        {
            IEnumerable<LdapEntry> entries;

            try
            {
                using var connection = new LdapConnection();
                connection.Connect(_options.LdapPath, 389, LdapSchema.LDAP);
                connection.SetOption(LdapOption.LDAP_OPT_REFERRALS, nint.Zero);
                await connection.BindAsync(LdapAuthType.Simple, new LdapCredential
                {
                    UserName = $"{username}@{_options.LdapPath}",
                    Password = password
                });
                entries = await connection.SearchAsync(_options.LdapDomain, $"(uid={username})");
            }
            catch (Exception e)
            {
                return null;
            }

            var entry = entries.FirstOrDefault();
            if (entry is null)
            {
                return null;
            }

            return entry;
        }

        public async Task<IEnumerable<LdapEntry>?> SearchUserAsync(string username)
        {
            IEnumerable<LdapEntry> entries;

            try
            {
                using var connection = new LdapConnection();
                connection.Connect(_options.LdapPath, 389, LdapSchema.LDAP);
                connection.SetOption(LdapOption.LDAP_OPT_REFERRALS, nint.Zero);
                await connection.BindAsync(LdapAuthType.Simple, new LdapCredential
                {
                    UserName = $"{_options.Username}@{_options.LdapPath}",
                    Password = _options.Password
                });
                entries = await connection.SearchAsync(_options.LdapDomain, $"(uid={username})");
            }
            catch (Exception e)
            {
                return null;
            }

            return entries;
        }
    }
}
