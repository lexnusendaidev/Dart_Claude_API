using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Repositories;
using DART_Claude.Models.Services;

namespace DART_Claude.Business.Services;

public sealed class LookupService : ILookupService
{
    private readonly ILookupRepository _lookupRepository;

    public LookupService(ILookupRepository lookupRepository)
    {
        _lookupRepository = lookupRepository;
    }

    public async Task<LookupsResponse> GetLookupsAsync(CancellationToken cancellationToken)
    {
        List<LookupItemResponse> appTypes = (await _lookupRepository.GetAppTypesAsync(cancellationToken))
            .Select(appType => new LookupItemResponse { Id = appType.TypeId, Name = appType.TypeName })
            .ToList();

        List<LookupItemResponse> criticalities = (await _lookupRepository.GetCriticalitiesAsync(cancellationToken))
            .Select(criticality => new LookupItemResponse { Id = criticality.CritId, Name = criticality.CritName })
            .ToList();

        List<LookupItemResponse> sdlcPhases = (await _lookupRepository.GetSdlcPhasesAsync(cancellationToken))
            .Select(sdlcPhase => new LookupItemResponse { Id = sdlcPhase.SdlcId, Name = sdlcPhase.SdlcName })
            .ToList();

        List<LookupItemResponse> pathTypes = (await _lookupRepository.GetPathTypesAsync(cancellationToken))
            .Select(pathType => new LookupItemResponse { Id = pathType.PathTypeId, Name = pathType.PathTypeName })
            .ToList();

        List<LookupItemResponse> employees = (await _lookupRepository.GetEmployeesAsync(cancellationToken))
            .Select(employee => new LookupItemResponse { Id = employee.EmpId, Name = employee.EmpPreferredName ?? string.Empty })
            .ToList();

        LookupsResponse response = new()
        {
            AppTypes = appTypes,
            Criticalities = criticalities,
            SdlcPhases = sdlcPhases,
            PathTypes = pathTypes,
            Employees = employees,
        };
        return response;
    }
}
