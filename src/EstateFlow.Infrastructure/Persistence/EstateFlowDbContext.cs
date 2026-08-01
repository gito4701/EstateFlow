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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
