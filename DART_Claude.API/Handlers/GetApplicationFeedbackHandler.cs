using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationFeedbackHandler : ServiceResultHandlerBase
{
    private readonly IFeedbackService _feedbackService;

    public GetApplicationFeedbackHandler(IFeedbackService feedbackService, ILogger<GetApplicationFeedbackHandler> logger)
        : base(logger)
    {
        _feedbackService = feedbackService;
    }

    public Task<ServiceResult<List<FeedbackResponse>>> HandleAsync(int applicationId, CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<FeedbackResponse>>> result = ExecuteAsync(
            () => _feedbackService.GetFeedbackForApplicationAsync(applicationId, cancellationToken),
            nameof(GetApplicationFeedbackHandler));
        return result;
    }
}
