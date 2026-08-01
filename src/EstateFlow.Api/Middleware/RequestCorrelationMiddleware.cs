using Microsoft.AspNetCore.Http;

namespace EstateFlow.Api.Middleware;

public sealed class RequestCorrelationMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-ID";
    private readonly RequestDelegate _next;

    public RequestCorrelationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = GetOrCreateCorrelationId(context);
        context.Items["CorrelationId"] = correlationId;

        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey(CorrelationIdHeaderName))
            {
                context.Response.Headers[CorrelationIdHeaderName] = correlationId;
            }

            return Task.CompletedTask;
        });

        await _next(context);
    }

    private static string GetOrCreateCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var providedCorrelationId) &&
            !string.IsNullOrWhiteSpace(providedCorrelationId.ToString()))
        {
            return providedCorrelationId.ToString();
        }

        return Guid.NewGuid().ToString("N");
    }
}
