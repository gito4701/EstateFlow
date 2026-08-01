using EstateFlow.Domain.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateFlow.Infrastructure.Persistence.Configurations;

public sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(property => property.Id);

        builder.Property(property => property.Id)
            .HasConversion(
                id => id.Value,
                value => new PropertyId(value));

        builder.Property(property => property.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(property => property.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(property => property.State)
            .IsRequired()
            .HasConversion<string>();
    }
}
