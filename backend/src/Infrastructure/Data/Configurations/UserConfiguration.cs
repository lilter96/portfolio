namespace Portfolio.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Entities;

    /// <summary>
    /// EF Core entity configuration for <see cref="User"/>.
    /// </summary>
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.DisplayName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired(false);
        }
    }
}
