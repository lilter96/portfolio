namespace Portfolio.Infrastructure.Caching
{
    using System.Text.Json;
    using Microsoft.Extensions.Caching.Distributed;
    using Portfolio.Domain.Interfaces;

    /// <summary>
    /// Redis-backed implementation of <see cref="ICacheService"/> using
    /// <see cref="IDistributedCache"/>.
    /// </summary>
    internal sealed class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache) => _cache = cache;

        public async Task<string?> GetAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            var value = await _cache.GetStringAsync(key, cancellationToken);
            return value;
        }

        public async Task SetAsync(
            string key,
            string value,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions();

            if (expiration is not null)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }

            await _cache.SetStringAsync(key, value, options, cancellationToken);
        }

        public async Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var testKey = "__health_check__";
                var testValue = DateTimeOffset.UtcNow.ToString("O");

                await _cache.SetStringAsync(
                    testKey,
                    testValue,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                    },
                    cancellationToken);

                var retrieved = await _cache.GetStringAsync(testKey, cancellationToken);
                return retrieved == testValue;
            }
            catch
            {
                return false;
            }
        }
    }
}
