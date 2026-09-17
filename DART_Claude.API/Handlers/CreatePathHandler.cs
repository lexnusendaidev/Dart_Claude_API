using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class CreatePathHandler : ServiceResultHandlerBase
{
    private readonly IPathService _pathService;

    public CreatePathHandler(IPathService pathService, ILogger<CreatePathHandler> logger)
        : base(logger)
    {
        _pathService = pathService;
    }

    public Task<ServiceResult<CreatePathResponse>> HandleAsync(
        int applicationId,
        CreatePathRequest request,
        CancellationToken cancellationToken)
    {
        Task<ServiceResult<CreatePathResponse>> result = ExecuteAsync(
            () => _pathService.CreatePathForApplicationAsync(applicationId, request, cancellationToken),
            nameof(CreatePathHandler),
            StatusCodes.Status201Created);
        return result;
    }
}
