using DART_Claude.Common.Constants;
using DART_Claude.Common.Exceptions;
using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class PathService : IPathService
{
    private readonly IPathRepository _pathRepository;
    private readonly IApplicationRepository _applicationRepository;

    public PathService(IPathRepository pathRepository, IApplicationRepository applicationRepository)
    {
        _pathRepository = pathRepository;
        _applicationRepository = applicationRepository;
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

    public async Task<CreatePathResponse> CreatePathForApplicationAsync(int applicationId, CreatePathRequest request, CancellationToken cancellationToken)
    {
        IApplicationListItem? application = await _applicationRepository.GetByIdAsync(applicationId, cancellationToken);
        if (application is null)
        {
            throw new EntityNotFoundException($"Application {applicationId} was not found.");
        }

        NewPath newPath = new()
        {
            ApplicationId = applicationId,
            PathTypeId = request.PathTypeId,
            PathLocation = request.PathLocation,
            CreatedByEmpId = PlaceholderIdentity.EmployeeId,
        };

        int id = await _pathRepository.CreateAsync(newPath, cancellationToken);
        CreatePathResponse response = new() { Id = id };
        return response;
    }
}
