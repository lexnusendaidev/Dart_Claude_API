namespace DART_Claude.Contracts.Responses;

public sealed class FeedbackFileResponse
{
    public long Id { get; init; }
    public string FilePathway { get; init; } = string.Empty;
}
