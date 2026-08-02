using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EstateFlow.Application.Requests;
using EstateFlow.Domain.Tenants;
using EstateFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Api.IntegrationTests;

public sealed class TenantsApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TenantsApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostApiTenants_WithValidRequest_ReturnsCreatedAndPersistedTenantId()
    {
        var request = new CreateTenantRequest("Sample Tenant");

        var response = await _client.PostAsJsonAsync("/api/tenants", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Contains("id", payload.Keys);
        Assert.NotNull(payload["id"]);
    }

    [Fact]
    public async Task PostApiTenants_WithInvalidRequest_ReturnsProblemDetails()
    {
        var response = await _client.PostAsJsonAsync("/api/tenants", new CreateTenantRequest(""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        Assert.NotNull(payload);
        Assert.Equal("One or more validation errors occurred.", payload!["title"].GetString());
    }

    [Fact]
    public async Task GetApiTenants_WithExistingTenant_ReturnsTenant()
    {
        var tenantId = TenantId.NewId();
        var tenant = Tenant.Create(tenantId, "Existing Tenant");

        await using var context = new EstateFlowDbContext(
            new DbContextOptionsBuilder<EstateFlowDbContext>()
                .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                .Options);

        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();

        var response = await _client.GetAsync($"/api/tenants/{tenantId.Value}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Equal(tenantId.Value.ToString(), payload!["id"]?.ToString());
        Assert.Equal("Existing Tenant", payload["name"]?.ToString());
    }

    [Fact]
    public async Task GetApiTenants_WithMissingTenant_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/tenants/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetApiTenants_ReturnsCollection()
    {
        await _client.PostAsJsonAsync("/api/tenants", new CreateTenantRequest("Tenant One"));
        await _client.PostAsJsonAsync("/api/tenants", new CreateTenantRequest("Tenant Two"));

        var response = await _client.GetAsync("/api/tenants");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
        Assert.NotNull(payload);
        Assert.True(payload!.Count >= 2);
    }
}
