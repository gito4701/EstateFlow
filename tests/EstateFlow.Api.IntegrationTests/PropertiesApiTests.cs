using System.Net;
using System.Net.Http.Json;
using EstateFlow.Application.Requests;
using EstateFlow.Domain.Properties;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Api.IntegrationTests;

public sealed class PropertiesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PropertiesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostApiProperties_WithValidRequest_ReturnsCreatedAndPersistedPropertyId()
    {
        var request = new CreatePropertyRequest("Sample Property", "123 Main St");

        var response = await _client.PostAsJsonAsync("/api/properties", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Contains("id", payload.Keys);
        Assert.NotNull(payload["id"]);
    }

    [Fact]
    public async Task PostApiProperties_WithInvalidRequest_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/properties", new CreatePropertyRequest("", ""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetApiProperties_WithExistingProperty_ReturnsProperty()
    {
        var propertyId = PropertyId.NewId();
        var property = Property.Create(propertyId, "Sample Property", "123 Main St");
        var repository = new EstateFlow.Infrastructure.Persistence.Repositories.PropertyRepository(
            new EstateFlow.Infrastructure.Persistence.EstateFlowDbContext(
                new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EstateFlow.Infrastructure.Persistence.EstateFlowDbContext>()
                    .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                    .Options));
        await repository.AddAsync(property);

        var response = await _client.GetAsync($"/api/properties/{propertyId.Value}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Equal(propertyId.Value.ToString(), payload!["id"]!.ToString());
    }

    [Fact]
    public async Task PutApiProperties_WithValidRequest_ReturnsUpdatedProperty()
    {
        var propertyId = PropertyId.NewId();
        var property = Property.Create(propertyId, "Original", "Original Address");
        var repository = new EstateFlow.Infrastructure.Persistence.Repositories.PropertyRepository(
            new EstateFlow.Infrastructure.Persistence.EstateFlowDbContext(
                new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EstateFlow.Infrastructure.Persistence.EstateFlowDbContext>()
                    .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                    .Options));
        await repository.AddAsync(property);

        var response = await _client.PutAsJsonAsync($"/api/properties/{propertyId.Value}", new UpdatePropertyRequest(propertyId, "Updated", "Updated Address"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
        Assert.NotNull(payload);
        Assert.Equal("Updated", payload!["name"]!.ToString());
        Assert.Equal("Updated Address", payload["address"]!.ToString());
    }

    [Fact]
    public async Task DeleteApiProperties_WithExistingProperty_ReturnsNoContent()
    {
        var propertyId = PropertyId.NewId();
        var property = Property.Create(propertyId, "Delete Me", "Delete Address");
        var repository = new EstateFlow.Infrastructure.Persistence.Repositories.PropertyRepository(
            new EstateFlow.Infrastructure.Persistence.EstateFlowDbContext(
                new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EstateFlow.Infrastructure.Persistence.EstateFlowDbContext>()
                    .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                    .Options));
        await repository.AddAsync(property);

        var response = await _client.DeleteAsync($"/api/properties/{propertyId.Value}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteApiProperties_WithMissingProperty_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync($"/api/properties/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteApiProperties_WithAlreadyArchivedProperty_ReturnsBadRequest()
    {
        var propertyId = PropertyId.NewId();
        var property = Property.Create(propertyId, "Delete Me", "Delete Address");
        property.Delete();
        var repository = new EstateFlow.Infrastructure.Persistence.Repositories.PropertyRepository(
            new EstateFlow.Infrastructure.Persistence.EstateFlowDbContext(
                new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EstateFlow.Infrastructure.Persistence.EstateFlowDbContext>()
                    .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                    .Options));
        await repository.AddAsync(property);

        var response = await _client.DeleteAsync($"/api/properties/{propertyId.Value}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutApiProperties_WithMissingProperty_ReturnsNotFound()
    {
        var response = await _client.PutAsJsonAsync($"/api/properties/{Guid.NewGuid()}", new UpdatePropertyRequest(PropertyId.NewId(), "Updated", "Updated Address"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PutApiProperties_WithInvalidRequest_ReturnsBadRequest()
    {
        var propertyId = PropertyId.NewId();
        var property = Property.Create(propertyId, "Original", "Original Address");
        var repository = new EstateFlow.Infrastructure.Persistence.Repositories.PropertyRepository(
            new EstateFlow.Infrastructure.Persistence.EstateFlowDbContext(
                new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EstateFlow.Infrastructure.Persistence.EstateFlowDbContext>()
                    .UseInMemoryDatabase("EstateFlow-IntegrationTests")
                    .Options));
        await repository.AddAsync(property);

        var response = await _client.PutAsJsonAsync($"/api/properties/{propertyId.Value}", new UpdatePropertyRequest(propertyId, "", ""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetApiProperties_WithMissingProperty_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/properties/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetApiProperties_ReturnsCollection()
    {
        await _client.PostAsJsonAsync("/api/properties", new CreatePropertyRequest("Property A", "Address A"));
        await _client.PostAsJsonAsync("/api/properties", new CreatePropertyRequest("Property B", "Address B"));

        var response = await _client.GetAsync("/api/properties");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
        Assert.NotNull(payload);
        Assert.True(payload.Count >= 2);
    }

    [Fact]
    public async Task GetHealth_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
