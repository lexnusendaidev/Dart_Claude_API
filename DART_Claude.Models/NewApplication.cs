namespace DART_Claude.Models;

public sealed class NewApplication
{
    public string Name { get; init; } = string.Empty;
    public decimal CurrentVersion { get; init; }
    public string? Description { get; init; }
    public int AppTypeId { get; init; }
    public int CriticalityId { get; init; }
    public short PrimaryDeveloperEmpId { get; init; }
    public short? SecondaryDeveloperEmpId { get; init; }
    public short? AnalystEmpId { get; init; }
    public short SdlcPhaseId { get; init; }
    public DateTime SdlcCheckDate { get; init; }
    public string FriendlyName { get; init; } = string.Empty;
    public bool AllowFeedback { get; init; }
    public short CreatedByEmpId { get; init; }
}
