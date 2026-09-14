namespace DART_Claude.Models;

public interface IDartFeedback
{
    int FbId { get; }
    string FbDescription { get; }
    bool FbIsActive { get; }
    bool? FbFollowupComplete { get; }
    bool? FbWasImplemented { get; }
    string? FbImplementedInVersion { get; }
    string? FbImplementedComments { get; }
    DateTime CreateDate { get; }
}
