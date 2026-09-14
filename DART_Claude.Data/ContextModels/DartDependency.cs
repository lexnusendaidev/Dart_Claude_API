using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartDependency
{
    public int DependAppId { get; set; }

    public int DependOnId { get; set; }

    public virtual DartApplication DependOn { get; set; } = null!;
}
