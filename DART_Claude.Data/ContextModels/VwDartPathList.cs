using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class VwDartPathList
{
    public int PathId { get; set; }

    public int ApplicationId { get; set; }

    public int PathTypeId { get; set; }

    public string PathTypeName { get; set; } = null!;

    public string PathLocation { get; set; } = null!;
}
