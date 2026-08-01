using EstateFlow.Api.Configuration;
using EstateFlow.Api.Middleware;
using EstateFlow.Application.Interfaces;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddSwaggerDocumentation();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value!.Errors.Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage) ? "The value is invalid." : error.ErrorMessage).ToArray());

        context.HttpContext.Items["ValidationErrors"] = errors;

        return new BadRequestObjectResult(new ValidationProblemDetails(errors)
        {
            Type = "about:blank",
            Title = "One or more validation errors occurred.",
            Detail = "The request contains invalid data.",
            Status = StatusCodes.Status400BadRequest
        });
    };
});

builder.Services.AddScoped<IApplicationService, ApiServiceRegistration>();
builder.Services.AddScoped<IInfrastructureService, ApiServiceRegistration>();
ApiServiceRegistration.RegisterServices(builder.Services);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<RequestCorrelationMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwaggerDocumentation();
app.UseRouting();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();
app.MapGet("/", () => Results.Ok(new { status = "EstateFlow API ready" }));

app.Run();

public partial class Program
{
}
