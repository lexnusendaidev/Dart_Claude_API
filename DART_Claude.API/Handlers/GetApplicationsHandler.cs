using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationsHandler : ServiceResultHandlerBase
{
    private readonly IApplicationService _applicationService;

    public GetApplicationsHandler(IApplicationService applicationService, ILogger<GetApplicationsHandler> logger)
        : base(logger)
    {
        _applicationService = applicationService;
    }

    public Task<ServiceResult<List<ApplicationResponse>>> HandleAsync(CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<ApplicationResponse>>> result = ExecuteAsync(
            () => _applicationService.GetApplicationsAsync(cancellationToken),
            nameof(GetApplicationsHandler));
        return result;
    }
}
