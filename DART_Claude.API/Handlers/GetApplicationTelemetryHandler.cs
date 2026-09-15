using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetApplicationTelemetryHandler : ServiceResultHandlerBase
{
    private readonly ITelemetryService _telemetryService;

    public GetApplicationTelemetryHandler(ITelemetryService telemetryService, ILogger<GetApplicationTelemetryHandler> logger)
        : base(logger)
    {
        _telemetryService = telemetryService;
    }

    public Task<ServiceResult<List<TelemetryResponse>>> HandleAsync(int applicationId, CancellationToken cancellationToken)
    {
        Task<ServiceResult<List<TelemetryResponse>>> result = ExecuteAsync(
            () => _telemetryService.GetTelemetryForApplicationAsync(applicationId, cancellationToken),
            nameof(GetApplicationTelemetryHandler));
        return result;
    }
}
