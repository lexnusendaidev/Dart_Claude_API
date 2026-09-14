using DART_Claude.Contracts.Responses;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<List<ApplicationResponse>> GetApplicationsAsync(CancellationToken cancellationToken)
    {
        List<IDartApplication> applications = await _applicationRepository.GetAllAsync(cancellationToken);
        List<ApplicationResponse> responses = applications
            .Select(application => new ApplicationResponse { Id = application.AppId, Name = application.AppName })
            .ToList();
        return responses;
    }
}
