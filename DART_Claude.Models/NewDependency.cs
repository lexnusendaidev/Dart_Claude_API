namespace DART_Claude.Models;

public sealed class NewDependency
{
    public int ApplicationId { get; init; }
    public int DependOnApplicationId { get; init; }
}
