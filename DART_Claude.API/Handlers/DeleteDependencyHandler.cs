using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class DeleteDependencyHandler : ServiceResultHandlerBase
{
    private readonly IDependencyService _dependencyService;

    public DeleteDependencyHandler(IDependencyService dependencyService, ILogger<DeleteDependencyHandler> logger)
        : base(logger)
    {
        _dependencyService = dependencyService;
    }

    public Task<ServiceResult<bool>> HandleAsync(
        int applicationId,
        int dependOnApplicationId,
        CancellationToken cancellationToken)
    {
        Task<ServiceResult<bool>> result = ExecuteAsync(
            () => _dependencyService.DeleteDependencyForApplicationAsync(applicationId, dependOnApplicationId, cancellationToken),
            nameof(DeleteDependencyHandler));
        return result;
    }
}
