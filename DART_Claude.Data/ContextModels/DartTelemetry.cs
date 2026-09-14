using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartTelemetry
{
    public int TeleId { get; set; }

    public int TeleAppId { get; set; }

    public string TeleMethodName { get; set; } = null!;

    public DateTime TeleCreateDate { get; set; }
}
