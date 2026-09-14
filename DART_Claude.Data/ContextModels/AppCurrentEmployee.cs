using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class AppCurrentEmployee
{
    public short EmpId { get; set; }

    public string? EmpPreferredName { get; set; }

    public virtual ICollection<DartApplication> DartApplicationAppAnalystEmps { get; set; } = new List<DartApplication>();

    public virtual ICollection<DartApplication> DartApplicationAppPrimDeveloperEmps { get; set; } = new List<DartApplication>();

    public virtual ICollection<DartApplication> DartApplicationAppSecondaryDeveloperEmps { get; set; } = new List<DartApplication>();

    public virtual ICollection<DartFeedback> DartFeedbacks { get; set; } = new List<DartFeedback>();
}
