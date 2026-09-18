using DART_Claude.Common.Exceptions;
using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class DependencyService : IDependencyService
{
    private readonly IDependencyRepository _dependencyRepository;
    private readonly IApplicationRepository _applicationRepository;

    public DependencyService(IDependencyRepository dependencyRepository, IApplicationRepository applicationRepository)
    {
        _dependencyRepository = dependencyRepository;
        _applicationRepository = applicationRepository;
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

    public async Task<DependencyResponse> CreateDependencyForApplicationAsync(int applicationId, CreateDependencyRequest request, CancellationToken cancellationToken)
    {
        if (request.DependOnApplicationId == applicationId)
        {
            throw new BusinessRuleException("An application cannot depend on itself.");
        }

        IApplicationListItem? application = await _applicationRepository.GetByIdAsync(applicationId, cancellationToken);
        if (application is null)
        {
            throw new EntityNotFoundException($"Application {applicationId} was not found.");
        }

        IApplicationListItem? dependOnApplication = await _applicationRepository.GetByIdAsync(request.DependOnApplicationId, cancellationToken);
        if (dependOnApplication is null)
        {
            throw new EntityNotFoundException($"Application {request.DependOnApplicationId} was not found.");
        }

        bool alreadyExists = await _dependencyRepository.ExistsAsync(applicationId, request.DependOnApplicationId, cancellationToken);
        if (alreadyExists)
        {
            throw new BusinessRuleException("This dependency already exists.");
        }

        NewDependency newDependency = new()
        {
            ApplicationId = applicationId,
            DependOnApplicationId = request.DependOnApplicationId,
        };
        await _dependencyRepository.CreateAsync(newDependency, cancellationToken);

        DependencyResponse response = new()
        {
            DependOnAppId = dependOnApplication.AppId,
            DependOnAppName = dependOnApplication.ApplicationName,
        };
        return response;
    }

    public async Task<bool> DeleteDependencyForApplicationAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken)
    {
        bool exists = await _dependencyRepository.ExistsAsync(applicationId, dependOnApplicationId, cancellationToken);
        if (!exists)
        {
            throw new EntityNotFoundException($"Application {applicationId} does not depend on application {dependOnApplicationId}.");
        }

        await _dependencyRepository.DeleteAsync(applicationId, dependOnApplicationId, cancellationToken);
        return true;
    }
}
