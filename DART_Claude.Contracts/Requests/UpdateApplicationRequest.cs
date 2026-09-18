using System.ComponentModel.DataAnnotations;

namespace DART_Claude.Contracts.Requests;

public sealed class UpdateApplicationRequest
{
    [Required]
    [StringLength(200)]
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

    [Required]
    [StringLength(200)]
    public string FriendlyName { get; init; } = string.Empty;

    public bool AllowFeedback { get; init; }
}
