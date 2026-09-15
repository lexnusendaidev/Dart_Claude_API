using DART_Claude.Contracts.Responses;

namespace DART_Claude.Models.Services;

public interface ILookupService
{
    Task<LookupsResponse> GetLookupsAsync(CancellationToken cancellationToken);
}
