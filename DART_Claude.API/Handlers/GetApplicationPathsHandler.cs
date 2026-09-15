using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationPathsHandler : ServiceResultHandlerBase
{
    private readonly IPathService _pathService;

    public GetApplicationPathsHandler(IPathService pathService, ILogger<GetApplicationPathsHandler> logger)
        : base(logger)
    {
        _pathService = pathService;
    }

    public Task<ServiceResult<List<PathResponse>>> HandleAsync(int applicationId, CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<PathResponse>>> result = ExecuteAsync(
            () => _pathService.GetPathsForApplicationAsync(applicationId, cancellationToken),
            nameof(GetApplicationPathsHandler));
        return result;
    }
}
