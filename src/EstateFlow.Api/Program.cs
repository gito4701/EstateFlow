using EstateFlow.Api.Configuration;
using EstateFlow.Application.Interfaces;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddRouting();

builder.Services.AddScoped<IApplicationService, ApiServiceRegistration>();
builder.Services.AddScoped<IInfrastructureService, ApiServiceRegistration>();
ApiServiceRegistration.RegisterServices(builder.Services);

builder.Services.AddDbContext<EstateFlowDbContext>(options =>
{
    options.UseInMemoryDatabase("EstateFlow");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();
app.MapGet("/", () => Results.Ok(new { status = "EstateFlow API ready" }));

app.Run();
