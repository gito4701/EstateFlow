using Microsoft.Extensions.Configuration;

namespace EstateFlow.Api.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddEstateFlowConfiguration(this IConfigurationBuilder builder, string contentRootPath, string environmentName)
    {
        var applicationBasePath = string.IsNullOrWhiteSpace(contentRootPath)
            ? AppContext.BaseDirectory
            : contentRootPath;

        builder.SetBasePath(applicationBasePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();

        return builder;
    }
}

