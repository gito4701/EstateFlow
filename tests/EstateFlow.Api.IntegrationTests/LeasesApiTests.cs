using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EstateFlow.Application.Requests;
using EstateFlow.Domain.Leases;
using EstateFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Api.IntegrationTests;

public sealed class LeasesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LeasesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostApiLeases_WithValidRequest_ReturnsCreatedAndPersistedLeaseId()
    {
        var request = new CreateLeaseRequest("Sample Lease");

        var response = await _client.PostAsJsonAsync("/api/leases", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Contains("id", payload.Keys);
        Assert.NotNull(payload["id"]);
    }

    [Fact]
    public async Task PostApiLeases_WithInvalidRequest_ReturnsProblemDetails()
    {
        var response = await _client.PostAsJsonAsync("/api/leases", new CreateLeaseRequest(""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        Assert.NotNull(payload);
        Assert.Equal("One or more validation errors occurred.", payload!["title"].GetString());
    }

    [Fact]
    public async Task GetApiLeases_WithExistingLease_ReturnsLease()
    {
        var leaseId = LeaseId.NewId();
        var lease = Lease.Create(leaseId, "Existing Lease");

        await using var context = new EstateFlowDbContext(
            new DbContextOptionsBuilder<EstateFlowDbContext>()
                .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                .Options);

        await context.Leases.AddAsync(lease);
        await context.SaveChangesAsync();

        var response = await _client.GetAsync($"/api/leases/{leaseId.Value}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Equal(leaseId.Value.ToString(), payload!["id"]?.ToString());
        Assert.Equal("Existing Lease", payload["name"]?.ToString());
    }

    [Fact]
    public async Task GetApiLeases_WithMissingLease_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/leases/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetApiLeases_ReturnsCollection()
    {
        await _client.PostAsJsonAsync("/api/leases", new CreateLeaseRequest("Lease One"));
        await _client.PostAsJsonAsync("/api/leases", new CreateLeaseRequest("Lease Two"));

        var response = await _client.GetAsync("/api/leases");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
        Assert.NotNull(payload);
        Assert.True(payload!.Count >= 2);
    }
}
