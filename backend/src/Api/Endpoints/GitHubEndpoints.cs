namespace Portfolio.Api.Endpoints
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Portfolio.Infrastructure.GitHub;

    /// <summary>
    /// Endpoint for live GitHub repository statistics, resiliently cached.
    /// </summary>
    internal static class GitHubEndpoints
    {
        internal static void MapGitHubEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/github")
                .HasApiVersion(1, 0);

            group
                .MapGet("/repos", GetFeaturedRepos)
                .WithName("GetGitHubRepos")
                .WithDescription(
                    "Returns live stats for featured lilter96 repositories. " +
                    "Cached for 5 minutes. Gracefully degrades to cached data if GitHub is unreachable.")
                .Produces<IReadOnlyList<GitHubRepoDto>>();
        }

        private static async Task<Ok<IReadOnlyList<GitHubRepoDto>>> GetFeaturedRepos(
            GitHubService service,
            CancellationToken cancellationToken)
        {
            var repos = await service.GetFeaturedReposAsync(cancellationToken);
            return TypedResults.Ok(repos);
        }
    }
}
