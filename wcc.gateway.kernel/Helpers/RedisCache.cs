using StackExchange.Redis;
using System.Text.Json;
using wcc.gateway.kernel.Interfaces;
using IDatabase = StackExchange.Redis.IDatabase;

namespace wcc.gateway.kernel.Helpers
{
    public class RedisCache : ICache
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly string _prefix;

        public RedisCache(string connectionString, string prefix = "cache:")
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
            _prefix = prefix;
        }

        private string GetRedisKey(string key) => $"{_prefix}{key}";

        // Add or update a value in the cache
        public async Task AddOrUpdateAsync<TValue>(string key, TValue value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(GetRedisKey(key), serializedValue);
        }

        // Try to get a value from the cache
        public async Task<TValue> TryGetValueAsync<TValue>(string key)
        {
            var redisValue = await _db.StringGetAsync(GetRedisKey(key));
            if (redisValue.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<TValue>(redisValue);
        }

        // Remove a value from the cache
        public async Task<bool> RemoveAsync(string key)
        {
            return await _db.KeyDeleteAsync(GetRedisKey(key));
        }

        // Clear all values from the cache
        public async Task ClearAsync()
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            foreach (var key in server.Keys(pattern: $"{_prefix}*"))
            {
                await _db.KeyDeleteAsync(key);
            }
        }
    }
}
