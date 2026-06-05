namespace Portfolio.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Entities;

    internal sealed class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("experiences");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Company).HasMaxLength(256).IsRequired();
            builder.Property(e => e.Role).HasMaxLength(256).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(4000).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired(false);
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasIndex(e => e.StartDate);
        }
    }
}
