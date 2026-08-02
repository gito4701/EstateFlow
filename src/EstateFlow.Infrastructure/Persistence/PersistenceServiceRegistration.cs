using EstateFlow.Application.Persistence;
using EstateFlow.Application.Services;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstateFlow.Infrastructure.Persistence;

/// <summary>
/// Registers the persistence services and repositories used by the application.
/// </summary>
public static class PersistenceServiceRegistration
{
    /// <summary>
    /// Adds the persistence layer services and repository implementations to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to populate.</param>
    /// <param name="configuration">The application configuration used to select persistence settings.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var persistenceOptions = new PersistenceOptions();
        configuration.GetSection("Persistence").Bind(persistenceOptions);

        services.AddSingleton(persistenceOptions);
        services.AddOptions<PersistenceOptions>().Configure(options =>
        {
            options.Provider = persistenceOptions.Provider;
            options.ConnectionString = persistenceOptions.ConnectionString;
        });

        services.AddDbContext<EstateFlowDbContext>((serviceProvider, options) =>
        {
            options.UseInMemoryDatabase(persistenceOptions.ConnectionString ?? "EstateFlow");
        });

        services.AddScoped<Infrastructure.Persistence.Abstractions.IPropertyRepository, PropertyRepository>();
        services.AddScoped<Application.Persistence.IPropertyRepository, PropertyRepository>();
        services.AddScoped<Infrastructure.Persistence.Abstractions.IOwnerRepository, OwnerRepository>();
        services.AddScoped<Application.Persistence.IOwnerRepository, OwnerRepository>();
        services.AddScoped<Infrastructure.Persistence.Abstractions.ITenantRepository, TenantRepository>();
        services.AddScoped<Application.Persistence.ITenantRepository, TenantRepository>();

        return services;
    }
}
