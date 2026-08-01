using EstateFlow.Application.Persistence;
using EstateFlow.Application.Services;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EstateFlow.Api.IntegrationTests;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Persistence:Provider"] = "InMemory",
                ["Persistence:ConnectionString"] = "EstateFlow-IntegrationTests"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<EstateFlowDbContext>>();
            services.RemoveAll<EstateFlowDbContext>();
            services.RemoveAll<IPropertyRepository>();
            services.RemoveAll<CreatePropertyService>();
            services.RemoveAll<GetPropertyService>();

            services.AddDbContext<EstateFlowDbContext>(options =>
            {
                options.UseInMemoryDatabase("EstateFlow-IntegrationTests");
            });

            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<CreatePropertyService>();
            services.AddScoped<GetPropertyService>();
        });
    }
}
