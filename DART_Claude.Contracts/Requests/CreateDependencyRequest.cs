namespace DART_Claude.Contracts.Requests;

public sealed class CreateDependencyRequest
{
    public int DependOnApplicationId { get; init; }
}
