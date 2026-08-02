using EstateFlow.Infrastructure.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EstateFlow.Infrastructure.Persistence;

public sealed class PersistenceHealthCheck : IHealthCheck
{
    private readonly EstateFlowDbContext _dbContext;

    public PersistenceHealthCheck(EstateFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            _ = _dbContext.Database.CanConnect();
            return Task.FromResult(HealthCheckResult.Healthy("Persistence is reachable"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Persistence check failed", ex));
        }
    }
}
