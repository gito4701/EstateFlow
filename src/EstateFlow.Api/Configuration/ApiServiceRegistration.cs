using EstateFlow.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EstateFlow.Api.Configuration;

/// <summary>
/// Registers application services for the API boundary.
/// </summary>
public static class ApiServiceRegistration
{
    /// <summary>
    /// Registers the application services used by the API controllers.
    /// </summary>
    /// <param name="services">The service collection to populate.</param>
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<CreatePropertyService>();
        services.AddScoped<GetPropertyService>();
        services.AddScoped<UpdatePropertyService>();
        services.AddScoped<DeletePropertyService>();
        services.AddScoped<SearchPropertiesService>();
        services.AddScoped<CreateOwnerService>();
        services.AddScoped<GetOwnerService>();
        services.AddScoped<CreateTenantService>();
        services.AddScoped<GetTenantService>();
        services.AddScoped<CreateLeaseService>();
        services.AddScoped<GetLeaseService>();
    }
}
