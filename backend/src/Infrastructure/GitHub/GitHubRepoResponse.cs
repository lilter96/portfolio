namespace Portfolio.Infrastructure.GitHub
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Raw response shape from GET /users/{user}/repos.
    /// Only the fields we surface are mapped; the rest are ignored.
    /// </summary>
    public sealed record GitHubRepoResponse(
        [property: JsonPropertyName("id")] long Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("full_name")] string FullName,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("html_url")] string HtmlUrl,
        [property: JsonPropertyName("language")] string? Language,
        [property: JsonPropertyName("stargazers_count")] int StargazersCount,
        [property: JsonPropertyName("forks_count")] int ForksCount,
        [property: JsonPropertyName("topics")] List<string>? Topics,
        [property: JsonPropertyName("updated_at")] DateTimeOffset UpdatedAt,
        [property: JsonPropertyName("pushed_at")] DateTimeOffset? PushedAt);
}
