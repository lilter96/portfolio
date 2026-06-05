namespace Portfolio.Infrastructure.Health
{
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Portfolio.Domain.Interfaces;

    /// <summary>
    /// Health check that verifies Redis connectivity via <see cref="ICacheService"/>.
    /// </summary>
    internal sealed class RedisHealthCheck : IHealthCheck
    {
        private readonly ICacheService _cache;

        public RedisHealthCheck(ICacheService cache) => _cache = cache;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var isHealthy = await _cache.IsHealthyAsync(cancellationToken);

            return isHealthy
                ? HealthCheckResult.Healthy("Redis is reachable")
                : HealthCheckResult.Unhealthy("Redis is unreachable");
        }
    }
}
