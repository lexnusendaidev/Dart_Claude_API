using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IFeedbackService
{
    Task<List<FeedbackResponse>> GetFeedbackForApplicationAsync(int applicationId, CancellationToken cancellationToken);
}
