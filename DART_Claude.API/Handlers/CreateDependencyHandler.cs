using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class CreateDependencyHandler : ServiceResultHandlerBase
{
    private readonly IDependencyService _dependencyService;

    public CreateDependencyHandler(IDependencyService dependencyService, ILogger<CreateDependencyHandler> logger)
        : base(logger)
    {
        _dependencyService = dependencyService;
    }

    public Task<ServiceResult<DependencyResponse>> HandleAsync(
        int applicationId,
        CreateDependencyRequest request,
        CancellationToken cancellationToken)
    {
        Task<ServiceResult<DependencyResponse>> result = ExecuteAsync(
            () => _dependencyService.CreateDependencyForApplicationAsync(applicationId, request, cancellationToken),
            nameof(CreateDependencyHandler),
            StatusCodes.Status201Created);
        return result;
    }
}
