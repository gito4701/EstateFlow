namespace EstateFlow.Api.Configuration;

public sealed class ApiBehaviorOptions
{
    public const string SectionName = "ApiBehavior";

    public string Title { get; set; } = "EstateFlow API";

    public string Detail { get; set; } = "EstateFlow Property Management API";

    public string Type { get; set; } = "about:blank";
}
