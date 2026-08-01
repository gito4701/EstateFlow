using EstateFlow.Domain.Properties;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence;

public class EstateFlowDbContext : DbContext
{
    public EstateFlowDbContext(DbContextOptions<EstateFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties => Set<Property>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(property => property.Id);
            entity.Property(property => property.Id)
                .HasConversion(
                    id => id.Value,
                    value => new PropertyId(value));

            entity.Property(property => property.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(property => property.Address)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(property => property.State)
                .IsRequired()
                .HasConversion<string>();
        });

        base.OnModelCreating(modelBuilder);
    }
}
