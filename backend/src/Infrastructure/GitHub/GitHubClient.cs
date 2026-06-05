namespace Portfolio.Infrastructure.GitHub
{
    using System.Net.Http.Json;
    using Microsoft.Extensions.Logging;
    using Polly;
    using Polly.CircuitBreaker;

    /// <summary>
    /// Typed HTTP client for the GitHub REST API with Polly resilience policies.
    /// Uses retry with exponential backoff and a circuit breaker to handle transient failures.
    /// </summary>
    public sealed class GitHubClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<GitHubClient> _logger;

        public GitHubClient(HttpClient http, ILogger<GitHubClient> logger)
        {
            _http = http;
            _logger = logger;
        }

        /// <summary>
        /// Fetches all public repositories for the given user.
        /// Returns an empty list if the circuit is open or all retries are exhausted.
        /// </summary>
        public async Task<IReadOnlyList<GitHubRepoResponse>> GetUserReposAsync(
            string username,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var repos = await _http.GetFromJsonAsync<List<GitHubRepoResponse>>(
                    $"users/{username}/repos?sort=updated&per_page=100&type=owner",
                    cancellationToken);

                return repos ?? [];
            }
            catch (BrokenCircuitException ex)
            {
                _logger.LogWarning(ex, "GitHub circuit breaker is open — returning empty");
                return [];
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "GitHub API request failed after retries — returning empty");
                return [];
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("GitHub API request timed out — returning empty");
                return [];
            }
        }
    }
}
