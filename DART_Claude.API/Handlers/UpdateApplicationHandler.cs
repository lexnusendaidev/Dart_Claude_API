using DART_Claude.Contracts.Requests;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class UpdateApplicationHandler : ServiceResultHandlerBase
{
    private readonly IApplicationService _applicationService;

    public UpdateApplicationHandler(IApplicationService applicationService, ILogger<UpdateApplicationHandler> logger)
        : base(logger)
    {
        _applicationService = applicationService;
    }

    public Task<ServiceResult<bool>> HandleAsync(int id, UpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        Task<ServiceResult<bool>> result = ExecuteAsync(
            () => _applicationService.UpdateApplicationAsync(id, request, cancellationToken),
            nameof(UpdateApplicationHandler));
        return result;
    }
}
