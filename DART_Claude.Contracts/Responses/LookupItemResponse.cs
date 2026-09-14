namespace DART_Claude.Contracts.Responses;

public sealed class LookupItemResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
