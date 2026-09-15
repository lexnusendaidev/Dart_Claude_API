using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IDependencyService
{
    Task<List<DependencyResponse>> GetDependenciesForApplicationAsync(int applicationId, CancellationToken cancellationToken);
}
