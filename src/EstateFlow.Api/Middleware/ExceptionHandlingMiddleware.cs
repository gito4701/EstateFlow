using System.Net;
using EstateFlow.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EstateFlow.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception while processing {Path}", context.Request.Path);

            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var response = ApiErrorResponse.Create(
                "Internal Server Error",
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred while processing the request.");

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
