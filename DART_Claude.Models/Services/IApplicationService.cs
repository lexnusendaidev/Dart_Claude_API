using DART_Claude.Contracts.Requests;
using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IApplicationService
{
    Task<List<ApplicationResponse>> GetApplicationsAsync(CancellationToken cancellationToken);
    Task<CreateApplicationResponse> CreateApplicationAsync(CreateApplicationRequest request, CancellationToken cancellationToken);
}
