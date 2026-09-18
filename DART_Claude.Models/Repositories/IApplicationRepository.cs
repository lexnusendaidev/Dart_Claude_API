using DART_Claude.Models;

namespace DART_Claude.Models.Repositories;

public interface IApplicationRepository
{
    Task<List<IApplicationListItem>> GetAllAsync(CancellationToken cancellationToken);
    Task<IApplicationListItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IApplicationDetail?> GetDetailByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateAsync(NewApplication application, CancellationToken cancellationToken);
    Task UpdateAsync(UpdatedApplication application, CancellationToken cancellationToken);
}
