using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class TelemetryService : ITelemetryService
{
    private readonly ITelemetryRepository _telemetryRepository;

    public TelemetryService(ITelemetryRepository telemetryRepository)
    {
        _telemetryRepository = telemetryRepository;
    }

    public async Task<List<TelemetryResponse>> GetTelemetryForApplicationAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<IDartTelemetry> telemetry = await _telemetryRepository.GetByApplicationIdAsync(applicationId, cancellationToken);
        List<TelemetryResponse> responses = telemetry
            .Select(item => new TelemetryResponse
            {
                Id = item.TeleId,
                MethodName = item.TeleMethodName,
                CreateDate = item.TeleCreateDate,
            })
            .ToList();
        return responses;
    }
}
