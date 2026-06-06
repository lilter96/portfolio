namespace Portfolio.Application.Tests.Dtos
{
    using FluentAssertions;
    using Portfolio.Application.Dtos;

    public sealed class DtoTests
    {
        [Fact]
        public void ProjectDto_CanBeCreated()
        {
            var dto = new ProjectDto(
                Id: Guid.NewGuid(),
                Title: "Test Project",
                Description: "A description",
                Url: "https://example.com",
                SourceUrl: null,
                Technologies: ["C#", ".NET"],
                SortOrder: 10,
                Status: "OpenSource",
                Role: "Solo developer",
                EvidenceUrl: "https://github.com/example");

            dto.Title.Should().Be("Test Project");
            dto.Technologies.Should().HaveCount(2).And.Contain(["C#", ".NET"]);
            dto.Url.Should().Be("https://example.com");
            dto.SourceUrl.Should().BeNull();
            dto.Status.Should().Be("OpenSource");
            dto.Role.Should().Be("Solo developer");
        }

        [Fact]
        public void ExperienceDto_CanBeCreated()
        {
            var dto = new ExperienceDto(
                Id: Guid.NewGuid(),
                Company: "Custom Games Studio",
                Role: "Senior .NET Backend Developer",
                Description: "Built slot backends",
                StartDate: new DateOnly(2022, 9, 1),
                EndDate: null);

            dto.Company.Should().Be("Custom Games Studio");
            dto.EndDate.Should().BeNull();
        }

        [Fact]
        public void SkillDto_CanBeCreated()
        {
            var dto = new SkillDto(
                Id: Guid.NewGuid(),
                Name: "C# / .NET",
                Category: "Backend",
                Proficiency: 95,
                SortOrder: 10,
                Evidence: "github.com/lilter96");

            dto.Name.Should().Be("C# / .NET");
            dto.Category.Should().Be("Backend");
            dto.Proficiency.Should().Be(95);
            dto.Evidence.Should().Be("github.com/lilter96");
        }

        [Fact]
        public void ProjectDto_AllowsNullUrls()
        {
            var dto = new ProjectDto(
                Id: Guid.NewGuid(),
                Title: "Internal Project",
                Description: "NDA work",
                Url: null,
                SourceUrl: null,
                Technologies: [],
                SortOrder: 0,
                Status: "WorkNda",
                Role: "Backend lead",
                EvidenceUrl: null);

            dto.Url.Should().BeNull();
            dto.SourceUrl.Should().BeNull();
            dto.Technologies.Should().BeEmpty();
            dto.Status.Should().Be("WorkNda");
        }
    }
}
