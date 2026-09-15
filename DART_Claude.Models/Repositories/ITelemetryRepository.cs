using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface ITelemetryRepository
{
    Task<List<IDartTelemetry>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken);
}
