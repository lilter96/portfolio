namespace Portfolio.Infrastructure.Tests.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Portfolio.Infrastructure.Data;
    using Portfolio.Infrastructure.Data.Seeding;

    /// <summary>
    /// Unit tests for <see cref="DbSeeder"/> using EF Core InMemory provider
    /// so no real database is needed.
    /// </summary>
    public sealed class DbSeederTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly DbSeeder _seeder;

        public DbSeederTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"portfolio_test_{Guid.NewGuid()}")
                .Options;

            _context = new AppDbContext(options);
            var logger = Mock.Of<ILogger<DbSeeder>>();
            _seeder = new DbSeeder(_context, logger);
        }

        public void Dispose() => _context.Dispose();

        [Fact]
        public async Task SeedAsync_EmptyDatabase_PopulatesAllTables()
        {
            await _seeder.SeedAsync();

            Assert.True(await _context.Users.AnyAsync());
            Assert.True(await _context.Experiences.AnyAsync());
            Assert.True(await _context.Skills.AnyAsync());
            Assert.True(await _context.Projects.AnyAsync());
        }

        [Fact]
        public async Task SeedAsync_CalledTwice_DoesNotDuplicate()
        {
            await _seeder.SeedAsync();
            var userCount = await _context.Users.CountAsync();
            var experienceCount = await _context.Experiences.CountAsync();
            var skillCount = await _context.Skills.CountAsync();
            var projectCount = await _context.Projects.CountAsync();

            // Seed again — should be idempotent
            await _seeder.SeedAsync();

            Assert.Equal(userCount, await _context.Users.CountAsync());
            Assert.Equal(experienceCount, await _context.Experiences.CountAsync());
            Assert.Equal(skillCount, await _context.Skills.CountAsync());
            Assert.Equal(projectCount, await _context.Projects.CountAsync());
        }

        [Fact]
        public async Task SeedAsync_SeedsCorrectNumberOfExperiences()
        {
            await _seeder.SeedAsync();

            Assert.Equal(6, await _context.Experiences.CountAsync());
        }

        [Fact]
        public async Task SeedAsync_SeedsCorrectNumberOfSkills()
        {
            await _seeder.SeedAsync();

            Assert.Equal(19, await _context.Skills.CountAsync());
        }

        [Fact]
        public async Task SeedAsync_SeedsCorrectNumberOfProjects()
        {
            await _seeder.SeedAsync();

            Assert.Equal(11, await _context.Projects.CountAsync());
        }
    }
}
