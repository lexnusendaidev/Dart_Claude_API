using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IPathRepository
{
    Task<List<IPathListItem>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken);
}
