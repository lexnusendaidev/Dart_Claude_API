using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetFeedbackFilesHandler : ServiceResultHandlerBase
{
    private readonly IFeedbackFileService _feedbackFileService;

    public GetFeedbackFilesHandler(IFeedbackFileService feedbackFileService, ILogger<GetFeedbackFilesHandler> logger)
        : base(logger)
    {
        _feedbackFileService = feedbackFileService;
    }

    public Task<ServiceResult<List<FeedbackFileResponse>>> HandleAsync(int feedbackId, CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<FeedbackFileResponse>>> result = ExecuteAsync(
            () => _feedbackFileService.GetFilesForFeedbackAsync(feedbackId, cancellationToken),
            nameof(GetFeedbackFilesHandler));
        return result;
    }
}
