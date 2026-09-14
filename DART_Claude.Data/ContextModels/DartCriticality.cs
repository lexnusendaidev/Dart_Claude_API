using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartCriticality
{
    public int CritId { get; set; }

    public string CritName { get; set; } = null!;

    public virtual ICollection<DartApplication> DartApplications { get; set; } = new List<DartApplication>();
}
