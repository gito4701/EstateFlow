namespace EstateFlow.Api.Responses;

public class ApiErrorResponse
{
    public string Type { get; init; } = "about:blank";
    public string Title { get; init; } = "Error";
    public int Status { get; init; }
    public string Detail { get; init; } = string.Empty;
    public string? TraceId { get; init; }

    public static ApiErrorResponse Create(string title, int status, string detail, string? traceId = null)
    {
        return new ApiErrorResponse
        {
            Type = "about:blank",
            Title = title,
            Status = status,
            Detail = detail,
            TraceId = traceId
        };
    }
}
