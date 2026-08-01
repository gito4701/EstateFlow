using EstateFlow.Application.Persistence;
using EstateFlow.Application.Services;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstateFlow.Infrastructure.Persistence;

public static class PersistenceServiceRegistration
{
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

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<CreatePropertyService>();

        return services;
    }
}
