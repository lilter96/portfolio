namespace Portfolio.Infrastructure.Tests.Caching
{
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Portfolio.Infrastructure.Caching;

    /// <summary>
    /// Unit tests for <see cref="RedisCacheService"/> using an in-memory
    /// <see cref="MemoryDistributedCache"/> so no Redis instance is needed.
    /// </summary>
    public sealed class RedisCacheServiceTests
    {
        private static MemoryDistributedCache CreateCache()
        {
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            return new MemoryDistributedCache(opts);
        }

        [Fact]
        public async Task SetAndGet_RoundTripsValue()
        {
            var cache = new RedisCacheService(CreateCache());

            await cache.SetAsync("key1", "hello-world");
            var result = await cache.GetAsync("key1");

            Assert.Equal("hello-world", result);
        }

        [Fact]
        public async Task Get_MissingKey_ReturnsNull()
        {
            var cache = new RedisCacheService(CreateCache());

            var result = await cache.GetAsync("no-such-key");

            Assert.Null(result);
        }

        [Fact]
        public async Task Set_WithExpiration_ExpiresValue()
        {
            var cache = new RedisCacheService(CreateCache());

            await cache.SetAsync("ephemeral", "gone-soon", TimeSpan.FromMilliseconds(1));

            // Wait for expiration
            await Task.Delay(50);

            var result = await cache.GetAsync("ephemeral");
            Assert.Null(result);
        }

        [Fact]
        public async Task Remove_DeletesValue()
        {
            var cache = new RedisCacheService(CreateCache());

            await cache.SetAsync("removable", "value");
            await cache.RemoveAsync("removable");

            var result = await cache.GetAsync("removable");
            Assert.Null(result);
        }

        [Fact]
        public async Task IsHealthy_WhenCacheWorks_ReturnsTrue()
        {
            var cache = new RedisCacheService(CreateCache());

            var healthy = await cache.IsHealthyAsync();

            Assert.True(healthy);
        }

        [Fact]
        public async Task Set_OverwritesExistingValue()
        {
            var cache = new RedisCacheService(CreateCache());

            await cache.SetAsync("key", "v1");
            await cache.SetAsync("key", "v2");

            Assert.Equal("v2", await cache.GetAsync("key"));
        }
    }
}
