using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface ITelemetryService
{
    Task<List<TelemetryResponse>> GetTelemetryForApplicationAsync(int applicationId, CancellationToken cancellationToken);
}
