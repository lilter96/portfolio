namespace Portfolio.Api.Tests.Integration
{
    using System.Net;
    using System.Net.Http.Json;
    using Portfolio.Application.Dtos;

    /// <summary>
    /// End-to-end integration tests using real PostgreSQL + Redis containers.
    /// Covers the main read endpoints and contact submission.
    /// </summary>
    [Collection("Integration")]
    public sealed class EndToEndTests : IClassFixture<PortfolioApiFactory>
    {
        private readonly HttpClient _client;

        public EndToEndTests(PortfolioApiFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Health_Liveness_ReturnsHealthy()
        {
            var response = await _client.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<HealthStatusResponse>();
            Assert.NotNull(body);
            Assert.Equal("Healthy", body.Status);
        }

        [Fact]
        public async Task Health_Readiness_ReturnsHealthyWithChecks()
        {
            var response = await _client.GetAsync("/health/ready");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<ReadinessResponse>();
            Assert.NotNull(body);
            Assert.Equal("Healthy", body.Status);
            Assert.Contains(body.Checks, c => c.Name == "postgres");
            Assert.Contains(body.Checks, c => c.Name == "redis");
        }

        [Fact]
        public async Task Projects_GetAll_ReturnsSeededProjects()
        {
            var response = await _client.GetAsync("/api/v1/projects");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var projects = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();

            Assert.NotNull(projects);
            Assert.NotEmpty(projects);
            Assert.Contains(projects, p => p.Title.Contains("TGSlots", StringComparison.Ordinal));
            Assert.Contains(projects, p => p.Title.Contains("Slot Math Lab", StringComparison.Ordinal));
            Assert.Contains(projects, p => p.Title.Contains("JobFinder", StringComparison.Ordinal));
        }

        [Fact]
        public async Task Experience_GetAll_ReturnsSeededExperience()
        {
            var response = await _client.GetAsync("/api/v1/experience");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var experiences = await response.Content.ReadFromJsonAsync<List<ExperienceDto>>();

            Assert.NotNull(experiences);
            Assert.NotEmpty(experiences);
            Assert.Contains(experiences, e => e.Company == "Custom Games Studio");
            Assert.Contains(experiences, e => e.Company == "Softeq");
        }

        [Fact]
        public async Task Skills_GetAll_ReturnsSeededSkills()
        {
            var response = await _client.GetAsync("/api/v1/skills");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var skills = await response.Content.ReadFromJsonAsync<List<SkillDto>>();

            Assert.NotNull(skills);
            Assert.NotEmpty(skills);
            Assert.Contains(skills, s => s.Name == "C# / .NET 10");
            Assert.Contains(skills, s => s.Name == "Exact probability / Monte Carlo");
        }

        [Fact]
        public async Task Contact_ValidSubmission_ReturnsAccepted()
        {
            var request = new
            {
                name = "Integration Test",
                email = "integration@test.com",
                message = "This is an end-to-end integration test submission."
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<ContactTestResponse>();
            Assert.NotNull(body);
            Assert.True(body.Accepted);
        }

        [Fact]
        public async Task Contact_InvalidEmail_ReturnsBadRequest()
        {
            var request = new
            {
                name = "Test",
                email = "not-an-email",
                message = "This message is long enough for validation."
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Contact_HoneypotFilled_StillReturnsOk()
        {
            var request = new
            {
                name = "Bot",
                email = "bot@spam.com",
                message = "Buy cheap stuff now! Click here for deals!",
                website = "http://evil-spam.com"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GitHub_Repos_GracefullyHandlesDegradation()
        {
            var response = await _client.GetAsync("/api/v1/github/repos");

            // The endpoint always returns 200 — either with live data (GitHub reachable)
            // or an empty list (GitHub rate-limited/unreachable, no cache available).
            // Both are valid graceful-degradation outcomes.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var repos = await response.Content.ReadFromJsonAsync<List<GitHubRepoDto>>();
            Assert.NotNull(repos);
            // Empty list is acceptable in CI where GitHub rate-limits Actions IPs
        }

        [Fact]
        public async Task OpenApi_Document_IsAccessible()
        {
            var response = await _client.GetAsync("/openapi/v1.json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    internal sealed record HealthStatusResponse(string Status);
    internal sealed record ReadinessCheck(string Name, string Status, string? Description);
    internal sealed record ReadinessResponse(string Status, List<ReadinessCheck> Checks);
    internal sealed record ContactTestResponse(bool Accepted, string? Message);
    internal sealed record GitHubRepoDto(string Name, string FullName, string? Description,
        string HtmlUrl, string? Language, int Stars, int Forks);
}
