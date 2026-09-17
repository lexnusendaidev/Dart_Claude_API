using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationByIdHandler : ServiceResultHandlerBase
{
    private readonly IApplicationService _applicationService;

    public GetApplicationByIdHandler(IApplicationService applicationService, ILogger<GetApplicationByIdHandler> logger)
        : base(logger)
    {
        _applicationService = applicationService;
    }

    public Task<ServiceResult<ApplicationResponse>> HandleAsync(int id, CancellationToken cancellationToken)
    {
        Task<ServiceResult<ApplicationResponse>> result = ExecuteAsync(
            () => _applicationService.GetApplicationByIdAsync(id, cancellationToken),
            nameof(GetApplicationByIdHandler));
        return result;
    }
}
