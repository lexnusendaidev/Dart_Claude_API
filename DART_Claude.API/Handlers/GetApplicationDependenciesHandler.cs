using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationDependenciesHandler : ServiceResultHandlerBase
{
    private readonly IDependencyService _dependencyService;

    public GetApplicationDependenciesHandler(IDependencyService dependencyService, ILogger<GetApplicationDependenciesHandler> logger)
        : base(logger)
    {
        _dependencyService = dependencyService;
    }

    public Task<ServiceResult<List<DependencyResponse>>> HandleAsync(int applicationId, CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<DependencyResponse>>> result = ExecuteAsync(
            () => _dependencyService.GetDependenciesForApplicationAsync(applicationId, cancellationToken),
            nameof(GetApplicationDependenciesHandler));
        return result;
    }
}
