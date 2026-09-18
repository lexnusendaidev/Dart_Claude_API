using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IApplicationService
{
    Task<List<ApplicationResponse>> GetApplicationsAsync(CancellationToken cancellationToken);
    Task<ApplicationResponse> GetApplicationByIdAsync(int id, CancellationToken cancellationToken);
    Task<ApplicationDetailResponse> GetApplicationDetailAsync(int id, CancellationToken cancellationToken);
    Task<CreateApplicationResponse> CreateApplicationAsync(CreateApplicationRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateApplicationAsync(int id, UpdateApplicationRequest request, CancellationToken cancellationToken);
}
