namespace DART_Claude.Contracts.Responses;

public sealed class ApplicationResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
