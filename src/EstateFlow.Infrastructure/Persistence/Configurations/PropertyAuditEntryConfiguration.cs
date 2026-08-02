using EstateFlow.Domain.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateFlow.Infrastructure.Persistence.Configurations;

public sealed class PropertyAuditEntryConfiguration : IEntityTypeConfiguration<PropertyAuditEntry>
{
    public void Configure(EntityTypeBuilder<PropertyAuditEntry> builder)
    {
        builder.ToTable("PropertyAuditEntries");

        builder.HasKey(entry => entry.Id);

        builder.Property(entry => entry.Id)
            .ValueGeneratedNever();

        builder.Property(entry => entry.PropertyId)
            .HasConversion(
                id => id.Value,
                value => new PropertyId(value));

        builder.Property(entry => entry.Operation)
            .HasConversion<string>();

        builder.Property(entry => entry.OccurredAtUtc)
            .IsRequired();

        builder.Property(entry => entry.Details)
            .HasMaxLength(500);
    }
}
