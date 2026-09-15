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
        List<IApplicationListItem> applications = await _applicationRepository.GetAllAsync(cancellationToken);
        List<ApplicationResponse> responses = applications
            .Select(application => new ApplicationResponse
            {
                Id = application.AppId,
                Name = application.ApplicationName,
                Criticality = application.Criticality,
                AppType = application.AppType,
                PrimaryDeveloper = application.PrimaryDeveloper,
                SecondaryDeveloper = application.SecondaryDeveloper,
                Analyst = application.Analyst,
                SdlcPhase = application.SdlcPhase,
                SdlcCheckDate = application.SdlcCheckDate,
            })
            .ToList();
        return responses;
    }
}
