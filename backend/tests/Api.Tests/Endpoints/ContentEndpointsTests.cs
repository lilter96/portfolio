namespace Portfolio.Api.Tests.Endpoints
{
    using System.Net;
    using System.Net.Http.Json;
    using Portfolio.Application.Dtos;

    /// <summary>
    /// Smoke tests for content endpoints (Projects, Experience, Skills).
    /// Runs against a running API instance. Start the API with:
    /// <c>dotnet run --project src/Api</c> before running these tests.
    /// </summary>
    public sealed class ContentEndpointsTests
    {
        private static readonly HttpClient Client = new()
        {
            BaseAddress = new Uri("http://localhost:5121")
        };

        [Fact]
        public async Task GetProjects_ReturnsOkWithValidDtos()
        {
            var response = await Client.GetAsync("/api/v1/projects");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var projects = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();

            Assert.NotNull(projects);
            Assert.NotEmpty(projects);
            Assert.All(projects, p =>
            {
                Assert.NotEqual(Guid.Empty, p.Id);
                Assert.False(string.IsNullOrWhiteSpace(p.Title));
                Assert.False(string.IsNullOrWhiteSpace(p.Description));
                Assert.NotEmpty(p.Technologies);
            });
        }

        [Fact]
        public async Task GetProjectById_ReturnsProject()
        {
            var list = await Client.GetFromJsonAsync<List<ProjectDto>>("/api/v1/projects");
            var firstId = list![0].Id;

            var project = await Client.GetFromJsonAsync<ProjectDto>($"/api/v1/projects/{firstId}");

            Assert.NotNull(project);
            Assert.Equal(firstId, project.Id);
        }

        [Fact]
        public async Task GetProjectById_NotFound_Returns404()
        {
            var response = await Client.GetAsync($"/api/v1/projects/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetExperience_ReturnsOkWithValidDtos()
        {
            var response = await Client.GetAsync("/api/v1/experience");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var experiences = await response.Content.ReadFromJsonAsync<List<ExperienceDto>>();

            Assert.NotNull(experiences);
            Assert.NotEmpty(experiences);
            Assert.All(experiences, e =>
            {
                Assert.NotEqual(Guid.Empty, e.Id);
                Assert.False(string.IsNullOrWhiteSpace(e.Company));
                Assert.False(string.IsNullOrWhiteSpace(e.Role));
            });
        }

        [Fact]
        public async Task GetSkills_ReturnsOkWithValidDtos()
        {
            var response = await Client.GetAsync("/api/v1/skills");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var skills = await response.Content.ReadFromJsonAsync<List<SkillDto>>();

            Assert.NotNull(skills);
            Assert.NotEmpty(skills);
            Assert.All(skills, s =>
            {
                Assert.NotEqual(Guid.Empty, s.Id);
                Assert.False(string.IsNullOrWhiteSpace(s.Name));
                Assert.False(string.IsNullOrWhiteSpace(s.Category));
                Assert.InRange(s.Proficiency, 0, 100);
            });
        }
    }
}
