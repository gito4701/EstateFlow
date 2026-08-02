using EstateFlow.Domain.Owners;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence;

public class EstateFlowDbContext : DbContext
{
    public EstateFlowDbContext(DbContextOptions<EstateFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Owner> Owners => Set<Owner>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new OwnerConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
