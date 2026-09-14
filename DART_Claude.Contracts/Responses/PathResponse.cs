namespace DART_Claude.Contracts.Responses;

public sealed class PathResponse
{
    public int PathId { get; init; }
    public string PathTypeName { get; init; } = string.Empty;
    public string PathLocation { get; init; } = string.Empty;
}
