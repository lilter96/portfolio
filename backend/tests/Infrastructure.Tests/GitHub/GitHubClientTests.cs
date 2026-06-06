namespace Portfolio.Infrastructure.Tests.GitHub
{
    using System.Net;
    using System.Net.Http.Json;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Portfolio.Infrastructure.GitHub;

    /// <summary>
    /// Unit tests for <see cref="GitHubClient"/> using a fake <see cref="HttpMessageHandler"/>
    /// so no network calls are made.
    /// </summary>
    public sealed class GitHubClientTests : IDisposable
    {
        private sealed class FakeHandler(HttpStatusCode statusCode, object? content = null)
            : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(statusCode);

                if (content is not null)
                {
                    response.Content = JsonContent.Create(content);
                }

                return Task.FromResult(response);
            }
        }

        private static GitHubClient CreateClient(HttpStatusCode status, object? content = null)
        {
            var handler = new FakeHandler(status, content);
            var http = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://api.github.com/")
            };
            var logger = Mock.Of<ILogger<GitHubClient>>();
            return new GitHubClient(http, logger);
        }

        private static GitHubRepoResponse MakeRepo(long id, string name, int stars = 1, int forks = 0)
        {
            return new GitHubRepoResponse(
                Id: id,
                Name: name,
                FullName: $"lilter96/{name}",
                Description: $"Description for {name}",
                HtmlUrl: $"https://github.com/lilter96/{name}",
                Language: "C#",
                StargazersCount: stars,
                ForksCount: forks,
                Topics: null,
                UpdatedAt: DateTimeOffset.UtcNow,
                PushedAt: DateTimeOffset.UtcNow);
        }

        public void Dispose()
        {
            // Clients created by CreateClient are all disposed here;
            // integration with xunit's disposal mechanism
        }

        [Fact]
        public async Task GetUserRepos_ReturnsParsedRepos()
        {
            var expectedRepos = new List<GitHubRepoResponse>
            {
                MakeRepo(1, "repo1", stars: 5),
                MakeRepo(2, "repo2", stars: 12),
            };
            var client = CreateClient(HttpStatusCode.OK, expectedRepos);

            var repos = await client.GetUserReposAsync("lilter96");

            Assert.Equal(2, repos.Count);
            Assert.Equal("repo1", repos[0].Name);
            Assert.Equal("repo2", repos[1].Name);
            Assert.Equal(5, repos[0].StargazersCount);
        }

        [Fact]
        public async Task GetUserRepos_HttpError_ReturnsEmpty()
        {
            var client = CreateClient(HttpStatusCode.InternalServerError);

            var repos = await client.GetUserReposAsync("error-user");

            Assert.Empty(repos);
        }

        [Fact]
        public async Task GetUserRepos_NotFound_ReturnsEmpty()
        {
            var client = CreateClient(HttpStatusCode.NotFound);

            var repos = await client.GetUserReposAsync("no-such-user-99999");

            Assert.Empty(repos);
        }

        [Fact]
        public async Task GetUserRepos_EmptyArrayResponse_ReturnsEmpty()
        {
            var client = CreateClient(HttpStatusCode.OK, Array.Empty<GitHubRepoResponse>());

            var repos = await client.GetUserReposAsync("lilter96");

            Assert.Empty(repos);
        }
    }
}
