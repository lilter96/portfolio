namespace Portfolio.Domain.Tests.Entities
{
    using Portfolio.Domain.Entities;

    public sealed class ProjectTests
    {
        [Fact]
        public void Create_AssignsAllProperties()
        {
            var project = new Project(
                "Test Project",
                "A test description",
                "https://example.com",
                "https://github.com/test/repo",
                ["C#", ".NET"],
                10);

            Assert.NotEqual(Guid.Empty, project.Id);
            Assert.Equal("Test Project", project.Title);
            Assert.Equal("A test description", project.Description);
            Assert.Equal("https://example.com", project.Url);
            Assert.Equal("https://github.com/test/repo", project.SourceUrl);
            Assert.Equal(["C#", ".NET"], project.Technologies);
            Assert.Equal(10, project.SortOrder);
            Assert.NotEqual(default, project.CreatedAt);
        }

        [Fact]
        public void Create_AllowsNullUrls()
        {
            var project = new Project(
                "Private Project",
                "No public URLs",
                null,
                null,
                ["F#"],
                20);

            Assert.Null(project.Url);
            Assert.Null(project.SourceUrl);
        }
    }
}
