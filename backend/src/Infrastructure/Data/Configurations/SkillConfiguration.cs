namespace Portfolio.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Entities;

    internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("skills");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.Property(s => s.Name).HasMaxLength(128).IsRequired();
            builder.Property(s => s.Category).HasMaxLength(128).IsRequired();
            builder.Property(s => s.Proficiency).IsRequired();
            builder.Property(s => s.SortOrder).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();

            builder.HasIndex(s => s.Category);
            builder.HasIndex(s => s.SortOrder);
        }
    }
}
