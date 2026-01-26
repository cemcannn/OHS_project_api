using Microsoft.Extensions.Caching.Memory;

namespace OHS_program_api.API.Services
{
    /// <summary>
    /// Cache service interface
    /// </summary>
    public interface ICacheService
    {
        T? Get<T>(string key);
        Task<T?> GetAsync<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        void Remove(string key);
        Task RemoveAsync(string key);
        void RemoveByPattern(string pattern);
        Task RemoveByPatternAsync(string pattern);
    }

    /// <summary>
    /// In-memory cache implementation
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<MemoryCacheService> _logger;
        private static readonly HashSet<string> _cacheKeys = new();

        public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public T? Get<T>(string key)
        {
            try
            {
                return _cache.TryGetValue(key, out T? value) ? value : default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cache get error for key: {key}");
                return default;
            }
        }

        public Task<T?> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            try
            {
                var options = new MemoryCacheEntryOptions();
                if (expiration.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = expiration;
                }
                else
                {
                    options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Default 30 min
                }

                _cache.Set(key, value, options);
                lock (_cacheKeys)
                {
                    _cacheKeys.Add(key);
                }

                _logger.LogInformation($"Cache set for key: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cache set error for key: {key}");
            }
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            Set(key, value, expiration);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            try
            {
                _cache.Remove(key);
                lock (_cacheKeys)
                {
                    _cacheKeys.Remove(key);
                }
                _logger.LogInformation($"Cache removed for key: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cache remove error for key: {key}");
            }
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }

        public void RemoveByPattern(string pattern)
        {
            try
            {
                var keysToRemove = _cacheKeys
                    .Where(k => k.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    Remove(key);
                }

                _logger.LogInformation($"Cache removed for pattern: {pattern} ({keysToRemove.Count} keys)");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cache remove by pattern error for pattern: {pattern}");
            }
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            RemoveByPattern(pattern);
            return Task.CompletedTask;
        }
    }
}
