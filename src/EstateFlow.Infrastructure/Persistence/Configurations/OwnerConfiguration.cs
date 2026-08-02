using EstateFlow.Domain.Owners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateFlow.Infrastructure.Persistence.Configurations;

public sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.HasKey(owner => owner.Id);

        builder.Property(owner => owner.Id)
            .HasConversion(
                id => id.Value,
                value => new OwnerId(value));

        builder.Property(owner => owner.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
