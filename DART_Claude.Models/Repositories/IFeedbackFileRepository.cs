using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IFeedbackFileRepository
{
    Task<List<IDartFeedbackFile>> GetByFeedbackIdAsync(int feedbackId, CancellationToken cancellationToken);
}
