using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class TelemetryRepository : ITelemetryRepository
{
    private readonly DartClaudeContext _context;

    public TelemetryRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IDartTelemetry>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<DartTelemetry> entities = await _context.DartTelemetries
            .Where(telemetry => telemetry.TeleAppId == applicationId)
            .ToListAsync(cancellationToken);
        List<IDartTelemetry> telemetry = entities.Cast<IDartTelemetry>().ToList();
        return telemetry;
    }
}
