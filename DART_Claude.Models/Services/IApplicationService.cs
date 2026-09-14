using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface IApplicationService
{
    Task<List<ApplicationResponse>> GetApplicationsAsync(CancellationToken cancellationToken);
}
