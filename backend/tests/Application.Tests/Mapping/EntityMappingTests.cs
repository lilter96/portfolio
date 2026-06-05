namespace Portfolio.Application.Tests.Mapping
{
    using Portfolio.Application.Mapping;
    using Portfolio.Domain.Entities;

    public sealed class EntityMappingTests
    {
        [Fact]
        public void Project_ToDto_MapsAllFields()
        {
            var entity = new Project("Title", "Desc", "https://url", "https://src", ["C#"], 10);

            var dto = entity.ToDto();

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal("Title", dto.Title);
            Assert.Equal("Desc", dto.Description);
            Assert.Equal("https://url", dto.Url);
            Assert.Equal("https://src", dto.SourceUrl);
            Assert.Equal(["C#"], dto.Technologies);
            Assert.Equal(10, dto.SortOrder);
        }

        [Fact]
        public void Experience_ToDto_MapsAllFields()
        {
            var start = new DateOnly(2023, 1, 1);
            var end = new DateOnly(2024, 6, 1);
            var entity = new Experience("Company", "Role", "Description", start, end);

            var dto = entity.ToDto();

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal("Company", dto.Company);
            Assert.Equal("Role", dto.Role);
            Assert.Equal("Description", dto.Description);
            Assert.Equal(start, dto.StartDate);
            Assert.Equal(end, dto.EndDate);
        }

        [Fact]
        public void Skill_ToDto_MapsAllFields()
        {
            var entity = new Skill("C#", "Backend", 85, 10);

            var dto = entity.ToDto();

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal("C#", dto.Name);
            Assert.Equal("Backend", dto.Category);
            Assert.Equal(85, dto.Proficiency);
            Assert.Equal(10, dto.SortOrder);
        }

        [Fact]
        public void ToDto_ThrowsOnNull()
        {
            Project? project = null;
            Assert.Throws<ArgumentNullException>(() => project!.ToDto());
        }
    }
}
