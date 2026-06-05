namespace Portfolio.Domain.Tests.Entities
{
    using Portfolio.Domain.Entities;

    public sealed class SkillTests
    {
        [Fact]
        public void Create_AssignsAllProperties()
        {
            var skill = new Skill("C#", "Backend", 95, 10);

            Assert.NotEqual(Guid.Empty, skill.Id);
            Assert.Equal("C#", skill.Name);
            Assert.Equal("Backend", skill.Category);
            Assert.Equal(95, skill.Proficiency);
            Assert.Equal(10, skill.SortOrder);
            Assert.NotEqual(default, skill.CreatedAt);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(100)]
        public void Create_AcceptsValidProficiencyRange(int proficiency)
        {
            var skill = new Skill("Go", "Backend", proficiency, 20);
            Assert.Equal(proficiency, skill.Proficiency);
        }
    }
}
