using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IPathService
{
    Task<List<PathResponse>> GetPathsForApplicationAsync(int applicationId, CancellationToken cancellationToken);
}
