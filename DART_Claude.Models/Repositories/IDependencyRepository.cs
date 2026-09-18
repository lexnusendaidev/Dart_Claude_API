using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IDependencyRepository
{
    Task<List<IDependencyListItem>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken);
    Task CreateAsync(NewDependency dependency, CancellationToken cancellationToken);
    Task DeleteAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken);
}
