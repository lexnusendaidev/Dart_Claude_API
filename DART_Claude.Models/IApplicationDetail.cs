namespace DART_Claude.Models;

public interface IApplicationDetail
{
    int Id { get; }
    string Name { get; }
    decimal CurrentVersion { get; }
    string? Description { get; }
    int AppTypeId { get; }
    int CriticalityId { get; }
    short PrimaryDeveloperEmpId { get; }
    short? SecondaryDeveloperEmpId { get; }
    short? AnalystEmpId { get; }
    short SdlcPhaseId { get; }
    DateTime SdlcCheckDate { get; }
    string FriendlyName { get; }
    bool AllowFeedback { get; }
}
