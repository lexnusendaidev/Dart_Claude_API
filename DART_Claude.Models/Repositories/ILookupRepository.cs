using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface ILookupRepository
{
    Task<List<IDartAppType>> GetAppTypesAsync(CancellationToken cancellationToken);
    Task<List<IDartCriticality>> GetCriticalitiesAsync(CancellationToken cancellationToken);
    Task<List<IDartSdlcPhase>> GetSdlcPhasesAsync(CancellationToken cancellationToken);
    Task<List<IDartPathType>> GetPathTypesAsync(CancellationToken cancellationToken);
    Task<List<IAppCurrentEmployee>> GetEmployeesAsync(CancellationToken cancellationToken);
}
