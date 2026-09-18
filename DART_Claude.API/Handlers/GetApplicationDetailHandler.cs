using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationDetailHandler : ServiceResultHandlerBase
{
    private readonly IApplicationService _applicationService;

    public GetApplicationDetailHandler(IApplicationService applicationService, ILogger<GetApplicationDetailHandler> logger)
        : base(logger)
    {
        _applicationService = applicationService;
    }

    public Task<ServiceResult<ApplicationDetailResponse>> HandleAsync(int id, CancellationToken cancellationToken)
    {
        Task<ServiceResult<ApplicationDetailResponse>> result = ExecuteAsync(
            () => _applicationService.GetApplicationDetailAsync(id, cancellationToken),
            nameof(GetApplicationDetailHandler));
        return result;
    }
}
