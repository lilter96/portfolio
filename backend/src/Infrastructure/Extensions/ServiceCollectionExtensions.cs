namespace Portfolio.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Polly;
    using Polly.Extensions.Http;
    using Portfolio.Domain.Interfaces;
    using Portfolio.Infrastructure.Caching;
    using Portfolio.Infrastructure.Data;
    using Portfolio.Infrastructure.Data.Seeding;
    using Portfolio.Infrastructure.GitHub;
    using Portfolio.Infrastructure.Health;

    /// <summary>
    /// Extension methods for registering infrastructure services in the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers EF Core with PostgreSQL, Redis cache, the seeder, GitHub client, and health checks.
        /// </summary>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── EF Core / PostgreSQL ────────────────────────
            var connectionString = configuration.GetConnectionString("Postgres");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // ── Redis ───────────────────────────────────────
            var redisConnection = configuration.GetConnectionString("Redis");

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "portfolio:";
            });

            services.AddSingleton<ICacheService, RedisCacheService>();

            // ── Database seeder ─────────────────────────────
            services.AddTransient<DbSeeder>();

            // ── GitHub client (Polly retry + circuit breaker) ──
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromMilliseconds(500 * Math.Pow(2, retryAttempt - 1)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30));

            services.AddHttpClient<GitHubClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("User-Agent", "lilter96-portfolio");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(circuitBreakerPolicy);

            services.AddSingleton<GitHubService>();

            // ── Health checks ───────────────────────────────
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
                .AddDbContextCheck<AppDbContext>("postgres", tags: ["ready"])
                .AddCheck<RedisHealthCheck>("redis", tags: ["ready"]);

            return services;
        }
    }
}
