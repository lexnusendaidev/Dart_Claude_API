namespace DART_Claude.Models;

public interface IDartTelemetry
{
    int TeleId { get; }
    string TeleMethodName { get; }
    DateTime TeleCreateDate { get; }
}
