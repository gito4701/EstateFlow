using EstateFlow.Application.Interfaces;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Services;
using EstateFlow.Infrastructure.Persistence.Repositories;
using EstateFlow.Infrastructure.Services;

namespace EstateFlow.Api.Configuration;

public sealed class ApiServiceRegistration : IApplicationService, IInfrastructureService
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<CreatePropertyService>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
    }
}
