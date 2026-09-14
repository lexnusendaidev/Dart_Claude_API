using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class VwDartApplicationList
{
    public int AppId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public string Criticality { get; set; } = null!;

    public string AppType { get; set; } = null!;

    public string? PrimaryDeveloper { get; set; }

    public string? SecondaryDeveloper { get; set; }

    public string? Analyst { get; set; }

    public string SdlcPhase { get; set; } = null!;

    public DateTime? SdlcCheckDate { get; set; }
}
