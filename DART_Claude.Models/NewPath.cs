namespace DART_Claude.Models;

public sealed class NewPath
{
    public int ApplicationId { get; init; }
    public int PathTypeId { get; init; }
    public string PathLocation { get; init; } = string.Empty;
    public short CreatedByEmpId { get; init; }
}
