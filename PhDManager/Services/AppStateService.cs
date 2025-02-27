using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace PhDManager.Services
{
    public class AppStateService(IDistributedCache cache)
    {
        private readonly IDistributedCache _cache = cache;
        private const string IsSystemOpenKey = "IsSystemOpenCache";

        public async Task SetSystemOpenAsync(bool isOpen)
        {
            await _cache.SetStringAsync(IsSystemOpenKey, JsonSerializer.Serialize(isOpen));
        }

        public async Task<bool> IsSystemOpenAsync()
        {
            var json = await _cache.GetStringAsync(IsSystemOpenKey);
            return json is not null && JsonSerializer.Deserialize<bool>(json);
        }
    }
}
