using DART_Claude.Common.Constants;
using DART_Claude.Common.Exceptions;
using DART_Claude.Contracts.Requests;
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

    public async Task<ApplicationResponse> GetApplicationByIdAsync(int id, CancellationToken cancellationToken)
    {
        IApplicationListItem? application = await _applicationRepository.GetByIdAsync(id, cancellationToken);
        if (application is null)
        {
            throw new EntityNotFoundException($"Application {id} was not found.");
        }

        ApplicationResponse response = new()
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
        };
        return response;
    }

    public async Task<ApplicationDetailResponse> GetApplicationDetailAsync(int id, CancellationToken cancellationToken)
    {
        IApplicationDetail? application = await _applicationRepository.GetDetailByIdAsync(id, cancellationToken);
        if (application is null)
        {
            throw new EntityNotFoundException($"Application {id} was not found.");
        }

        ApplicationDetailResponse response = new()
        {
            Id = application.Id,
            Name = application.Name,
            CurrentVersion = application.CurrentVersion,
            Description = application.Description,
            AppTypeId = application.AppTypeId,
            CriticalityId = application.CriticalityId,
            PrimaryDeveloperEmpId = application.PrimaryDeveloperEmpId,
            SecondaryDeveloperEmpId = application.SecondaryDeveloperEmpId,
            AnalystEmpId = application.AnalystEmpId,
            SdlcPhaseId = application.SdlcPhaseId,
            SdlcCheckDate = application.SdlcCheckDate,
            FriendlyName = application.FriendlyName,
            AllowFeedback = application.AllowFeedback,
        };
        return response;
    }

    public async Task<CreateApplicationResponse> CreateApplicationAsync(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        NewApplication newApplication = new()
        {
            Name = request.Name,
            CurrentVersion = request.CurrentVersion,
            Description = request.Description,
            AppTypeId = request.AppTypeId,
            CriticalityId = request.CriticalityId,
            PrimaryDeveloperEmpId = request.PrimaryDeveloperEmpId,
            SecondaryDeveloperEmpId = request.SecondaryDeveloperEmpId,
            AnalystEmpId = request.AnalystEmpId,
            SdlcPhaseId = request.SdlcPhaseId,
            SdlcCheckDate = request.SdlcCheckDate,
            FriendlyName = request.FriendlyName,
            AllowFeedback = request.AllowFeedback,
            CreatedByEmpId = PlaceholderIdentity.EmployeeId,
        };

        int id = await _applicationRepository.CreateAsync(newApplication, cancellationToken);
        CreateApplicationResponse response = new() { Id = id };
        return response;
    }

    public async Task<bool> UpdateApplicationAsync(int id, UpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        IApplicationDetail? existingApplication = await _applicationRepository.GetDetailByIdAsync(id, cancellationToken);
        if (existingApplication is null)
        {
            throw new EntityNotFoundException($"Application {id} was not found.");
        }

        UpdatedApplication updatedApplication = new()
        {
            Id = id,
            Name = request.Name,
            CurrentVersion = request.CurrentVersion,
            Description = request.Description,
            AppTypeId = request.AppTypeId,
            CriticalityId = request.CriticalityId,
            PrimaryDeveloperEmpId = request.PrimaryDeveloperEmpId,
            SecondaryDeveloperEmpId = request.SecondaryDeveloperEmpId,
            AnalystEmpId = request.AnalystEmpId,
            SdlcPhaseId = request.SdlcPhaseId,
            SdlcCheckDate = request.SdlcCheckDate,
            FriendlyName = request.FriendlyName,
            AllowFeedback = request.AllowFeedback,
            UpdatedByEmpId = PlaceholderIdentity.EmployeeId,
        };

        await _applicationRepository.UpdateAsync(updatedApplication, cancellationToken);
        return true;
    }
}
