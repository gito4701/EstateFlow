namespace EstateFlow.Api.Middleware;

public sealed class CorrelationContext
{
    public string Id { get; init; } = string.Empty;
}
