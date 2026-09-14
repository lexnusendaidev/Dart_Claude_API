using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartPath
{
    public int PathId { get; set; }

    public int PathAppId { get; set; }

    public int PathTypeId { get; set; }

    public string PathLocation { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public short CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public short? UpdateBy { get; set; }
}
