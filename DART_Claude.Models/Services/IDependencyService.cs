using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IDependencyService
{
    Task<List<DependencyResponse>> GetDependenciesForApplicationAsync(int applicationId, CancellationToken cancellationToken);
    Task<DependencyResponse> CreateDependencyForApplicationAsync(int applicationId, CreateDependencyRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteDependencyForApplicationAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken);
}
