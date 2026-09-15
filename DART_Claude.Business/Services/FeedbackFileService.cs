using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class FeedbackFileService : IFeedbackFileService
{
    private readonly IFeedbackFileRepository _feedbackFileRepository;

    public FeedbackFileService(IFeedbackFileRepository feedbackFileRepository)
    {
        _feedbackFileRepository = feedbackFileRepository;
    }

    public async Task<List<FeedbackFileResponse>> GetFilesForFeedbackAsync(int feedbackId, CancellationToken cancellationToken)
    {
        List<IDartFeedbackFile> files = await _feedbackFileRepository.GetByFeedbackIdAsync(feedbackId, cancellationToken);
        List<FeedbackFileResponse> responses = files
            .Select(file => new FeedbackFileResponse
            {
                Id = file.FfId,
                FilePathway = file.FfFilePathway,
            })
            .ToList();
        return responses;
    }
}
