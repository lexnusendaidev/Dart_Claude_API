using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class DependencyService : IDependencyService
{
    private readonly IDependencyRepository _dependencyRepository;

    public DependencyService(IDependencyRepository dependencyRepository)
    {
        _dependencyRepository = dependencyRepository;
    }

    public async Task<List<DependencyResponse>> GetDependenciesForApplicationAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<IDependencyListItem> dependencies = await _dependencyRepository.GetByApplicationIdAsync(applicationId, cancellationToken);
        List<DependencyResponse> responses = dependencies
            .Select(dependency => new DependencyResponse
            {
                DependOnAppId = dependency.DependOnAppId,
                DependOnAppName = dependency.DependOnAppName,
            })
            .ToList();
        return responses;
    }
}
