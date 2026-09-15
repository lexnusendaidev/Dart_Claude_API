namespace DART_Claude.Models;

public interface IDependencyListItem
{
    int DependOnAppId { get; }
    string DependOnAppName { get; }
}
