using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IDependencyRepository
{
    Task<List<IDependencyListItem>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken);
}
