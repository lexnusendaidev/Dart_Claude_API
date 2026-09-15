namespace DART_Claude.Models;

public interface IApplicationListItem
{
    int AppId { get; }
    string ApplicationName { get; }
    string Criticality { get; }
    string AppType { get; }
    string? PrimaryDeveloper { get; }
    string? SecondaryDeveloper { get; }
    string? Analyst { get; }
    string SdlcPhase { get; }
    DateTime? SdlcCheckDate { get; }
}
