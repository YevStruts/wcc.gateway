namespace wcc.gateway.kernel.Interfaces
{
    public interface ICache
    {
        Task AddOrUpdateAsync<TValue>(string key, TValue value);
        Task AddOrUpdateAsync<TValue>(string key, TValue value, TimeSpan expiry);
        Task<TValue> TryGetValueAsync<TValue>(string key);
        Task<bool> RemoveAsync(string key);
        Task ClearAsync();
    }
}
