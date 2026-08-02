using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace EstateFlow.Api.IntegrationTests;

public sealed class HealthEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HealthEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealthReady_ReturnsOk()
    {
        var response = await _client.GetAsync("/health/ready");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetHealthReady_IncludesPersistenceDiagnosticCheck()
    {
        var response = await _client.GetAsync("/health/ready");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        Assert.NotNull(payload);

        var checks = payload!["checks"].EnumerateArray().ToList();
        Assert.Contains(checks, check => check.GetProperty("name").GetString() == "self");
        Assert.Contains(checks, check => check.GetProperty("name").GetString() == "persistence");
    }
}
