using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IFeedbackFileService
{
    Task<List<FeedbackFileResponse>> GetFilesForFeedbackAsync(int feedbackId, CancellationToken cancellationToken);
}
