namespace DART_Claude.Contracts.Responses;

public sealed class TelemetryResponse
{
    public int Id { get; init; }
    public string MethodName { get; init; } = string.Empty;
    public DateTime CreateDate { get; init; }
}
