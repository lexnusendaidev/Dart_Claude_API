using DART_Claude.Contracts.Responses;
using DART_Claude.Models.Services;

namespace DART_Claude.API.Handlers;

public sealed class GetLookupsHandler : ServiceResultHandlerBase
{
    private readonly ILookupService _lookupService;

    public GetLookupsHandler(ILookupService lookupService, ILogger<GetLookupsHandler> logger)
        : base(logger)
    {
        _lookupService = lookupService;
    }

    public Task<ServiceResult<LookupsResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        Task<ServiceResult<LookupsResponse>> result = ExecuteAsync(
            () => _lookupService.GetLookupsAsync(cancellationToken),
            nameof(GetLookupsHandler));
        return result;
    }
}
