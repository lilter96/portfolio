namespace Portfolio.Domain.Tests.Entities
{
    using FluentAssertions;
    using Portfolio.Domain.Entities;

    public sealed class ExperienceTests
    {
        [Fact]
        public void Create_AssignsAllProperties()
        {
            var start = new DateOnly(2022, 9, 1);

            var exp = new Experience(
                "Custom Games Studio",
                "Senior .NET Backend Developer",
                "Built slot backends on myKonami/Aristocrat",
                start,
                null);

            exp.Id.Should().NotBeEmpty();
            exp.Company.Should().Be("Custom Games Studio");
            exp.Role.Should().Be("Senior .NET Backend Developer");
            exp.Description.Should().Be("Built slot backends on myKonami/Aristocrat");
            exp.StartDate.Should().Be(start);
            exp.EndDate.Should().BeNull();
            exp.CreatedAt.Should().NotBe(default(DateTimeOffset));
        }

        [Fact]
        public void Create_WithEndDate_StoresBothDates()
        {
            var start = new DateOnly(2021, 6, 1);
            var end = new DateOnly(2022, 8, 31);

            var exp = new Experience("Solvintech", "Fullstack Dev", "Description", start, end);

            exp.StartDate.Should().Be(start);
            exp.EndDate.Should().Be(end);
        }

        [Fact]
        public void Create_CurrentRole_HasNullEndDate()
        {
            var exp = new Experience(
                "Company",
                "Role",
                "Description",
                new DateOnly(2023, 1, 1),
                null);

            exp.EndDate.Should().BeNull();
        }

        [Fact]
        public void Create_EachInstance_HasUniqueId()
        {
            var exp1 = new Experience("A", "R", "D", new DateOnly(2020, 1, 1), null);
            var exp2 = new Experience("B", "R", "D", new DateOnly(2021, 1, 1), null);

            exp1.Id.Should().NotBe(exp2.Id);
        }
    }
}
