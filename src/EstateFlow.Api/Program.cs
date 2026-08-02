using EstateFlow.Api.Configuration;
using EstateFlow.Api.Middleware;
using EstateFlow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEstateFlowConfiguration(builder.Environment.ContentRootPath, builder.Environment.EnvironmentName);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("API is ready"));
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerDocumentation();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
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

builder.Services.Configure<EstateFlow.Api.Configuration.ApiBehaviorOptions>(builder.Configuration.GetSection("ApiBehavior"));

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
app.UseRouting();
app.UseHttpsRedirection();
app.UseHsts();
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    context.Response.Headers["Referrer-Policy"] = "no-referrer";
    context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; frame-ancestors 'self'";
    await next();
});
app.UseAuthorization();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = WriteHealthResponse
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Name == "self",
    ResponseWriter = WriteHealthResponse
});
app.MapControllers();
app.MapGet("/", () => Results.Ok(new { status = "EstateFlow API ready" }));

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.Run();

static Task WriteHealthResponse(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";

    var payload = new
    {
        status = report.Status.ToString(),
        checks = report.Entries.Select(entry => new
        {
            name = entry.Key,
            status = entry.Value.Status.ToString(),
            description = entry.Value.Description
        })
    };

    return context.Response.WriteAsJsonAsync(payload);
}

public partial class Program
{
}
