namespace Portfolio.Infrastructure.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Portfolio.Domain.Entities;

    /// <summary>
    /// Application database context. Owns the aggregate roots and applies
    /// entity configurations from the same assembly.
    /// </summary>
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<Skill> Skills => Set<Skill>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
