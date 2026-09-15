namespace DART_Claude.Contracts.Responses;

public sealed class LookupsResponse
{
    public List<LookupItemResponse> AppTypes { get; init; } = [];
    public List<LookupItemResponse> Criticalities { get; init; } = [];
    public List<LookupItemResponse> SdlcPhases { get; init; } = [];
    public List<LookupItemResponse> PathTypes { get; init; } = [];
    public List<LookupItemResponse> Employees { get; init; } = [];
}
