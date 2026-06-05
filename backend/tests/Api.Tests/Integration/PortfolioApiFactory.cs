namespace Portfolio.Api.Tests.Integration
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Portfolio.Domain.Interfaces;
    using Portfolio.Infrastructure.Email;
    using Testcontainers.PostgreSql;
    using Testcontainers.Redis;

    /// <summary>
    /// Custom WebApplicationFactory that replaces PostgreSQL and Redis with Testcontainers.
    /// Seeds the database and provides a pre-configured HttpClient for integration tests.
    /// </summary>
    public sealed class PortfolioApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
            .WithDatabase("portfolio_test")
            .WithUsername("portfolio")
            .WithPassword("test_pass")
            .Build();

        private readonly RedisContainer _redis = new RedisBuilder()
            .Build();

        public string PostgresConnectionString => _postgres.GetConnectionString();
        public string RedisConnectionString => _redis.GetConnectionString();

        async Task IAsyncLifetime.InitializeAsync()
        {
            await _postgres.StartAsync();
            await _redis.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _postgres.DisposeAsync();
            await _redis.DisposeAsync();
            await base.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.UseSetting("ConnectionStrings:Postgres", _postgres.GetConnectionString());
            builder.UseSetting("ConnectionStrings:Redis", _redis.GetConnectionString());

            builder.ConfigureTestServices(services =>
            {
                // Keep NoOpEmailSender for integration tests (no real SMTP)
                services.RemoveAll<IEmailSender>();
                services.AddSingleton<IEmailSender, NoOpEmailSender>();
            });
        }
    }
}
