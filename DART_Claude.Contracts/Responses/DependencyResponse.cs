namespace DART_Claude.Contracts.Responses;

public sealed class DependencyResponse
{
    public int DependOnAppId { get; init; }
    public string DependOnAppName { get; init; } = string.Empty;
}
