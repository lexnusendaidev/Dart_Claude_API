using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<List<FeedbackResponse>> GetFeedbackForApplicationAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<IDartFeedback> feedback = await _feedbackRepository.GetByApplicationIdAsync(applicationId, cancellationToken);
        List<FeedbackResponse> responses = feedback
            .Select(item => new FeedbackResponse
            {
                Id = item.FbId,
                Description = item.FbDescription,
                IsActive = item.FbIsActive,
                FollowupComplete = item.FbFollowupComplete,
                WasImplemented = item.FbWasImplemented,
                ImplementedInVersion = item.FbImplementedInVersion,
                ImplementedComments = item.FbImplementedComments,
                CreateDate = item.CreateDate,
            })
            .ToList();
        return responses;
    }
}
