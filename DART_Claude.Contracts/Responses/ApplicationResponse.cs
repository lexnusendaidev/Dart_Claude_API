namespace DART_Claude.Contracts.Responses;

public sealed class ApplicationResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Criticality { get; init; } = string.Empty;
    public string AppType { get; init; } = string.Empty;
    public string? PrimaryDeveloper { get; init; }
    public string? SecondaryDeveloper { get; init; }
    public string? Analyst { get; init; }
    public string SdlcPhase { get; init; } = string.Empty;
    public DateTime? SdlcCheckDate { get; init; }
}
