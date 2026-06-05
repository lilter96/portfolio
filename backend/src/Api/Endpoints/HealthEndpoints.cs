namespace Portfolio.Api.Endpoints
{
    using System.ComponentModel;
    using Microsoft.AspNetCore.Http.HttpResults;

    /// <summary>
    /// Sample Minimal API endpoint group demonstrating typed results and API versioning.
    /// </summary>
    internal static class HealthEndpoints
    {
        internal static void MapHealthEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/health")
                .HasApiVersion(1, 0);

            group
                .MapGet("/", GetHealth)
                .WithName("GetHealth")
                .WithDescription("Returns the current health status of the API.");

            group
                .MapGet("/ping", Ping)
                .WithName("Ping")
                .WithDescription("Simple ping/pong endpoint with typed result.");

            // Error demo — deliberately throws to exercise ProblemDetails
            group
                .MapGet("/error", ThrowError)
                .WithName("ErrorDemo")
                .WithDescription("Demonstrates the global error handler returning ProblemDetails.");
        }

        internal static void MapReadinessEndpoints(this WebApplication app)
        {
            app.MapHealthChecks("/health/ready", new()
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(e => new
                        {
                            Name = e.Key,
                            Status = e.Value.Status.ToString(),
                            e.Value.Description
                        })
                    });
                }
            });
        }

        /// <summary>
        /// Returns health status with server timestamp (typed result).
        /// </summary>
        [Description("Health check")]
        private static Ok<HealthResponse> GetHealth()
        {
            var response = new HealthResponse(
                Status: "Healthy",
                Timestamp: DateTimeOffset.UtcNow,
                Version: "1.0");
            return TypedResults.Ok(response);
        }

        /// <summary>
        /// Simple ping/pong for connectivity checks.
        /// </summary>
        [Description("Ping/pong")]
        private static Ok<PingResponse> Ping()
        {
            return TypedResults.Ok(new PingResponse(Pong: true, Timestamp: DateTimeOffset.UtcNow));
        }

        /// <summary>
        /// Throws an exception to demonstrate ProblemDetails error responses.
        /// </summary>
        [Description("Demonstrate error handling")]
        private static Ok<HealthResponse> ThrowError()
        {
            throw new InvalidOperationException(
                "This is a demonstration error. The global exception handler should convert " +
                "this into an RFC 7807 ProblemDetails response.");
        }
    }

    /// <summary>
    /// Health check response.
    /// </summary>
    internal sealed record HealthResponse(string Status, DateTimeOffset Timestamp, string Version);

    /// <summary>
    /// Ping response.
    /// </summary>
    internal sealed record PingResponse(bool Pong, DateTimeOffset Timestamp);
}
