using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartAppType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<DartApplication> DartApplications { get; set; } = new List<DartApplication>();
}
