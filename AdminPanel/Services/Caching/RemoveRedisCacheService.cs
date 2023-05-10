using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace AdminPanel.Services.Caching;

public class RemoveRedisCacheService : IRemoveCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConfiguration _configuration;

    public RemoveRedisCacheService(IDistributedCache distributedCache, IConfiguration configuration)
    {
        _distributedCache = distributedCache;
        _configuration = configuration;
    }

    public async Task Remove(string pattern)
    {
        List<string> listKeys;
        var connectionString = _configuration.GetConnectionString("Redis");
        var host = connectionString.Substring(0, connectionString.IndexOf(':'));
        var port = int.Parse(connectionString.Substring(connectionString.IndexOf(':') + 1));

        using (var redis = await ConnectionMultiplexer.ConnectAsync(connectionString))
        {
            listKeys = redis
            .GetServer(host, port)
            .Keys()
                .Select(key => (string)key)
                .Where(x => x.StartsWith(pattern))
            .ToList();
        }

        foreach (var x in listKeys)
        {
            await _distributedCache.RemoveAsync(x);
        }
    }
}