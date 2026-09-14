using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IApplicationRepository
{
    Task<List<IApplicationListItem>> GetAllAsync(CancellationToken cancellationToken);
}
