using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EstateFlow.Application.Requests;
using EstateFlow.Domain.Owners;
using EstateFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Api.IntegrationTests;

public sealed class OwnersApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OwnersApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostApiOwners_WithValidRequest_ReturnsCreatedAndPersistedOwnerId()
    {
        var request = new CreateOwnerRequest("Sample Owner");

        var response = await _client.PostAsJsonAsync("/api/owners", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Contains("id", payload.Keys);
        Assert.NotNull(payload["id"]);
    }

    [Fact]
    public async Task PostApiOwners_WithInvalidRequest_ReturnsProblemDetails()
    {
        var response = await _client.PostAsJsonAsync("/api/owners", new CreateOwnerRequest(""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        Assert.NotNull(payload);
        Assert.Equal("One or more validation errors occurred.", payload!["title"].GetString());
    }

    [Fact]
    public async Task GetApiOwners_WithExistingOwner_ReturnsOwner()
    {
        var ownerId = OwnerId.NewId();
        var owner = Owner.Create(ownerId, "Existing Owner");

        await using var context = new EstateFlowDbContext(
            new DbContextOptionsBuilder<EstateFlowDbContext>()
                .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                .Options);

        await context.Owners.AddAsync(owner);
        await context.SaveChangesAsync();

        var response = await _client.GetAsync($"/api/owners/{ownerId.Value}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Equal(ownerId.Value.ToString(), payload!["id"]?.ToString());
        Assert.Equal("Existing Owner", payload["name"]?.ToString());
    }

    [Fact]
    public async Task GetApiOwners_WithMissingOwner_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/owners/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetApiOwners_ReturnsCollection()
    {
        await _client.PostAsJsonAsync("/api/owners", new CreateOwnerRequest("Owner One"));
        await _client.PostAsJsonAsync("/api/owners", new CreateOwnerRequest("Owner Two"));

        var response = await _client.GetAsync("/api/owners");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
        Assert.NotNull(payload);
        Assert.True(payload!.Count >= 2);
    }
}
