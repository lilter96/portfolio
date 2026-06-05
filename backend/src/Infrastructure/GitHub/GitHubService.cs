namespace Portfolio.Infrastructure.GitHub
{
    using System.Text.Json;
    using Microsoft.Extensions.Logging;
    using Portfolio.Domain.Interfaces;

    /// <summary>
    /// Orchestrates fetching GitHub repo stats with Redis caching.
    /// Cache TTL is intentionally short (5 minutes) so stats stay reasonably fresh
    /// without hammering the GitHub API.
    /// </summary>
    public sealed class GitHubService
    {
        private readonly GitHubClient _client;
        private readonly ICacheService _cache;
        private readonly ILogger<GitHubService> _logger;

        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);
        private const string CacheKey = "github:repos:lilter96";

        public GitHubService(
            GitHubClient client,
            ICacheService cache,
            ILogger<GitHubService> logger)
        {
            _client = client;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Returns featured repos for lilter96, preferring cached data.
        /// Falls back to stale cache if GitHub is unreachable.
        /// </summary>
        public async Task<IReadOnlyList<GitHubRepoDto>> GetFeaturedReposAsync(
            CancellationToken cancellationToken = default)
        {
            // Try live fetch first
            var liveRepos = await _client.GetUserReposAsync("lilter96", cancellationToken);

            if (liveRepos.Count > 0)
            {
                var featured = FilterAndMap(liveRepos);

                // Cache the successful response
                var json = JsonSerializer.Serialize(featured);
                await _cache.SetAsync(CacheKey, json, CacheTtl, cancellationToken);

                _logger.LogInformation(
                    "Fetched {Count} repos from GitHub, {Featured} featured",
                    liveRepos.Count,
                    featured.Count);

                return featured;
            }

            // Live fetch failed — try stale cache
            var cached = await _cache.GetAsync(CacheKey, cancellationToken);

            if (cached is not null)
            {
                var stale = JsonSerializer.Deserialize<List<GitHubRepoDto>>(cached);
                _logger.LogInformation("Returned {Count} repos from cache (GitHub unreachable)", stale?.Count ?? 0);
                return stale ?? [];
            }

            _logger.LogWarning("No live or cached GitHub data available");
            return [];
        }

        /// <summary>
        /// The configured cache TTL (for verification and headers).
        /// </summary>
        public static TimeSpan CacheTtlValue => CacheTtl;

        private static List<GitHubRepoDto> FilterAndMap(IEnumerable<GitHubRepoResponse> repos)
        {
            // Featured repos — the ones we surface on the portfolio
            var featured = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "crypto-exchange-rates",
                "realtime_chat",
                "MekashronTest",
                "pull-requests-status-check",
                "picture_finder",
                "color_converter",
            };

            return repos
                .Where(r => featured.Contains(r.Name))
                .OrderByDescending(r => r.StargazersCount)
                .Select(r => new GitHubRepoDto(
                    r.Name,
                    r.FullName,
                    r.Description,
                    r.HtmlUrl,
                    r.Language,
                    r.StargazersCount,
                    r.ForksCount,
                    r.Topics ?? [],
                    r.UpdatedAt,
                    r.PushedAt))
                .ToList();
        }
    }

    /// <summary>
    /// Public DTO for a featured GitHub repository.
    /// </summary>
    public sealed record GitHubRepoDto(
        string Name,
        string FullName,
        string? Description,
        string HtmlUrl,
        string? Language,
        int Stars,
        int Forks,
        List<string> Topics,
        DateTimeOffset UpdatedAt,
        DateTimeOffset? PushedAt);
}
