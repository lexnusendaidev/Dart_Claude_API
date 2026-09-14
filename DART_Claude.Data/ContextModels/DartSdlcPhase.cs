using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartSdlcPhase
{
    public short SdlcId { get; set; }

    public string SdlcName { get; set; } = null!;

    public virtual ICollection<DartApplication> DartApplications { get; set; } = new List<DartApplication>();
}
