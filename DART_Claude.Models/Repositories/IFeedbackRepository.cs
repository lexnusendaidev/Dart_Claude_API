using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IFeedbackRepository
{
    Task<List<IDartFeedback>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken);
}
