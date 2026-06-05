namespace Portfolio.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Entities;

    internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Title).HasMaxLength(256).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(4000).IsRequired();
            builder.Property(p => p.Url).HasMaxLength(2048);
            builder.Property(p => p.SourceUrl).HasMaxLength(2048);
            builder.Property(p => p.Technologies).HasColumnType("jsonb").IsRequired();
            builder.Property(p => p.SortOrder).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasIndex(p => p.SortOrder);
        }
    }
}
