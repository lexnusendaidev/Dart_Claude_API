namespace DART_Claude.Contracts.Responses;

public sealed class FeedbackResponse
{
    public int Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public bool? FollowupComplete { get; init; }
    public bool? WasImplemented { get; init; }
    public string? ImplementedInVersion { get; init; }
    public string? ImplementedComments { get; init; }
    public DateTime CreateDate { get; init; }
}
