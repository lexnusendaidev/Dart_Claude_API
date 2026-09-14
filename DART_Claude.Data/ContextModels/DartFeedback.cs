using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartFeedback
{
    public int FbId { get; set; }

    public int FbAppId { get; set; }

    public string FbDescription { get; set; } = null!;

    public bool FbIsActive { get; set; }

    public bool? FbFollowupComplete { get; set; }

    public bool? FbWasImplemented { get; set; }

    public string? FbImplementedInVersion { get; set; }

    public DateTime CreateDate { get; set; }

    public short CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public short? UpdateBy { get; set; }

    public string? FbImplementedComments { get; set; }

    public virtual AppCurrentEmployee CreateByNavigation { get; set; } = null!;
}
