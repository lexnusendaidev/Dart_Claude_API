using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class VwDartDependencyList
{
    public int DependAppId { get; set; }

    public int DependOnAppId { get; set; }

    public string DependOnAppName { get; set; } = null!;
}
