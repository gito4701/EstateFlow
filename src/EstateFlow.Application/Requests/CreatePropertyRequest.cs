namespace EstateFlow.Application.Requests;

public sealed record CreatePropertyRequest(string Name, string Address) : ApplicationRequest;
