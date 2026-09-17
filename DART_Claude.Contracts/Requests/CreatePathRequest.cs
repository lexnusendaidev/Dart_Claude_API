using System.ComponentModel.DataAnnotations;

namespace DART_Claude.Contracts.Requests;

public sealed class CreatePathRequest
{
    public int PathTypeId { get; init; }

    [Required]
    [StringLength(500)]
    public string PathLocation { get; init; } = string.Empty;
}
