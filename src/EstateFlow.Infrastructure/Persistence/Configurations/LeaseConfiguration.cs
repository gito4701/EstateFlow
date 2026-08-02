using EstateFlow.Domain.Leases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateFlow.Infrastructure.Persistence.Configurations;

public sealed class LeaseConfiguration : IEntityTypeConfiguration<Lease>
{
    public void Configure(EntityTypeBuilder<Lease> builder)
    {
        builder.ToTable("Leases");

        builder.HasKey(lease => lease.Id);

        builder.Property(lease => lease.Id)
            .HasConversion(
                id => id.Value,
                value => new LeaseId(value));

        builder.Property(lease => lease.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
