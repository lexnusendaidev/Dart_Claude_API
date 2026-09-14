using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class PathService : IPathService
{
    private readonly IPathRepository _pathRepository;

    public PathService(IPathRepository pathRepository)
    {
        _pathRepository = pathRepository;
    }

    public async Task<List<PathResponse>> GetPathsForApplicationAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<IPathListItem> paths = await _pathRepository.GetByApplicationIdAsync(applicationId, cancellationToken);
        List<PathResponse> responses = paths
            .Select(path => new PathResponse
            {
                PathId = path.PathId,
                PathTypeName = path.PathTypeName,
                PathLocation = path.PathLocation,
            })
            .ToList();
        return responses;
    }
}
