using System.Diagnostics;
using System.Globalization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Endpoints;
using Portfolio.Api.Middleware;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Data.Seeding;
using Portfolio.Infrastructure.Email;
using Portfolio.Infrastructure.Extensions;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "Portfolio.Api")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting API host");

    var builder = WebApplication.CreateBuilder(args);

    // ── Logging ───────────────────────────────────────────
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                formatProvider: CultureInfo.InvariantCulture));

    // ── CORS ──────────────────────────────────────────────
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("FrontendOrigin", policy =>
            policy
                .WithOrigins("http://localhost:3000")
                .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
                .WithHeaders("Content-Type", "Authorization")
                .WithExposedHeaders("X-RateLimit-Remaining", "X-RateLimit-Reset"));
    });

    // ── Rate Limiting ─────────────────────────────────────
    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 60,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                }));

        // Contact form: strict per-IP cap to deter spam
        options.AddPolicy("Contact", context =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                }));

        options.OnRejected = async (context, cancellationToken) =>
        {
            context.HttpContext.Response.Headers["Retry-After"] =
                context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                    ? retryAfter.ToString()
                    : "60";

            await context.HttpContext.Response.WriteAsJsonAsync(
                new
                {
                    type = "https://tools.ietf.org/html/rfc6585#section-4",
                    title = "Too Many Requests",
                    status = 429,
                    detail = "Rate limit exceeded. Please wait before making further requests.",
                    retryAfter = context.HttpContext.Response.Headers["Retry-After"].ToString()
                },
                cancellationToken);
        };
    });

    // ── Response Compression ──────────────────────────────
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });

    builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
        options.Level = System.IO.Compression.CompressionLevel.Fastest);

    // ── Email sender (dev: no-op, prod: swap to SMTP) ──────
    builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();

    // ── FluentValidation ──────────────────────────────────
    builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>(
        ServiceLifetime.Singleton,
        includeInternalTypes: true);

    // ── API versioning ────────────────────────────────────
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    // ── OpenAPI ───────────────────────────────────────────
    builder.Services.AddOpenApi();

    // ── Data & cache infrastructure ───────────────────────
    builder.Services.AddInfrastructure(builder.Configuration);

    // ── ProblemDetails (RFC 7807) ─────────────────────────
    builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Instance =
                $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
            context.ProblemDetails.Extensions["traceId"] =
                Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
        });

    var app = builder.Build();

    // ── Middleware pipeline ───────────────────────────────
    app.UseExceptionHandler();
    app.UseStatusCodePages();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
        app.UseHsts();
    }

    app.UseSecurityHeaders();
    app.UseRateLimiter();
    app.UseCors("FrontendOrigin");
    app.UseResponseCompression();

    // ── Health checks ─────────────────────────────────────
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("live"),
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                Status = report.Status.ToString()
            });
        }
    });

    app.MapReadinessEndpoints();

    // ── OpenAPI / Scalar ──────────────────────────────────
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Portfolio API")
            .WithTheme(ScalarTheme.BluePlanet)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    // ── Endpoints ─────────────────────────────────────────
    app.MapHealthEndpoints();
    app.MapUserEndpoints();
    app.MapProjectsEndpoints();
    app.MapExperienceEndpoints();
    app.MapSkillsEndpoints();
    app.MapContactEndpoints();
    app.MapGitHubEndpoints();

    // ── Database migration & seeding ──────────────────────
    await using (var scope = app.Services.CreateAsyncScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        Log.Information("Database migrations applied");

        var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await seeder.SeedAsync();
    }

    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "API host terminated unexpectedly");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

return 0;
