using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class LookupRepository : ILookupRepository
{
    private readonly DartClaudeContext _context;

    public LookupRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IDartAppType>> GetAppTypesAsync(CancellationToken cancellationToken)
    {
        List<DartAppType> entities = await _context.DartAppTypes.ToListAsync(cancellationToken);
        List<IDartAppType> appTypes = entities.Cast<IDartAppType>().ToList();
        return appTypes;
    }

    public async Task<List<IDartCriticality>> GetCriticalitiesAsync(CancellationToken cancellationToken)
    {
        List<DartCriticality> entities = await _context.DartCriticalities.ToListAsync(cancellationToken);
        List<IDartCriticality> criticalities = entities.Cast<IDartCriticality>().ToList();
        return criticalities;
    }

    public async Task<List<IDartSdlcPhase>> GetSdlcPhasesAsync(CancellationToken cancellationToken)
    {
        List<DartSdlcPhase> entities = await _context.DartSdlcPhases.ToListAsync(cancellationToken);
        List<IDartSdlcPhase> sdlcPhases = entities.Cast<IDartSdlcPhase>().ToList();
        return sdlcPhases;
    }

    public async Task<List<IDartPathType>> GetPathTypesAsync(CancellationToken cancellationToken)
    {
        List<DartPathType> entities = await _context.DartPathTypes.ToListAsync(cancellationToken);
        List<IDartPathType> pathTypes = entities.Cast<IDartPathType>().ToList();
        return pathTypes;
    }

    public async Task<List<IAppCurrentEmployee>> GetEmployeesAsync(CancellationToken cancellationToken)
    {
        List<AppCurrentEmployee> entities = await _context.AppCurrentEmployees.ToListAsync(cancellationToken);
        List<IAppCurrentEmployee> employees = entities.Cast<IAppCurrentEmployee>().ToList();
        return employees;
    }
}
