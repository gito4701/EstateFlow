namespace EstateFlow.Application.Requests;

public sealed record SearchPropertiesRequest(string? Name = null, string? Status = null, int Page = 1, int PageSize = 10, string? Sort = null, string? Direction = null) : ApplicationRequest;
