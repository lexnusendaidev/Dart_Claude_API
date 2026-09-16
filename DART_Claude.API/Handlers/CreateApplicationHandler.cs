using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class CreateApplicationHandler : ServiceResultHandlerBase
{
    private readonly IApplicationService _applicationService;

    public CreateApplicationHandler(IApplicationService applicationService, ILogger<CreateApplicationHandler> logger)
        : base(logger)
    {
        _applicationService = applicationService;
    }

    public Task<ServiceResult<CreateApplicationResponse>> HandleAsync(
        CreateApplicationRequest request,
        CancellationToken cancellationToken)
    {
        Task<ServiceResult<CreateApplicationResponse>> result = ExecuteAsync(
            () => _applicationService.CreateApplicationAsync(request, cancellationToken),
            nameof(CreateApplicationHandler),
            StatusCodes.Status201Created);
        return result;
    }
}
