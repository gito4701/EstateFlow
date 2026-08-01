using EstateFlow.Api.Configuration;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace EstateFlow.Api.Tests;

public class EnvironmentConfigurationTests
{
    [Fact]
    public void AddEstateFlowConfiguration_LoadsEnvironmentSpecificSettings()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDirectory);

        try
        {
            File.WriteAllText(Path.Combine(tempDirectory, "appsettings.json"), """
            {
              "ApiBehavior": {
                "Title": "Base title",
                "Detail": "Base detail",
                "Type": "about:blank"
              }
            }
            """);

            File.WriteAllText(Path.Combine(tempDirectory, "appsettings.Development.json"), """
            {
              "ApiBehavior": {
                "Title": "Development title",
                "Detail": "Development detail",
                "Type": "development"
              }
            }
            """);

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddEstateFlowConfiguration(tempDirectory, "Development");

            var configuration = configurationBuilder.Build();

            Assert.Equal("Development title", configuration["ApiBehavior:Title"]);
            Assert.Equal("Development detail", configuration["ApiBehavior:Detail"]);
            Assert.Equal("development", configuration["ApiBehavior:Type"]);
        }
        finally
        {
            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, recursive: true);
            }
        }
    }
}
