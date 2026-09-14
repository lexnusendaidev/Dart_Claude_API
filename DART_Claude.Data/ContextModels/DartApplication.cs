using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartApplication
{
    public int AppId { get; set; }

    public string AppName { get; set; } = null!;

    public decimal AppCurrentVersion { get; set; }

    public string? AppDescription { get; set; }

    public int AppType { get; set; }

    public int AppCriticalityId { get; set; }

    public short AppPrimDeveloperEmpId { get; set; }

    public short? AppSecondaryDeveloperEmpId { get; set; }

    public short? AppAnalystEmpId { get; set; }

    public short AppSdlcPhaseId { get; set; }

    public DateTime AppSdlcCheckDate { get; set; }

    public DateTime CreateDate { get; set; }

    public short CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public short? UpdateBy { get; set; }

    public string AppFriendlyName { get; set; } = null!;

    public bool AppAllowFeedback { get; set; }

    public virtual AppCurrentEmployee? AppAnalystEmp { get; set; }

    public virtual DartCriticality AppCriticality { get; set; } = null!;

    public virtual AppCurrentEmployee AppPrimDeveloperEmp { get; set; } = null!;

    public virtual DartSdlcPhase AppSdlcPhase { get; set; } = null!;

    public virtual AppCurrentEmployee? AppSecondaryDeveloperEmp { get; set; }

    public virtual DartAppType AppTypeNavigation { get; set; } = null!;

    public virtual ICollection<DartDependency> DartDependencies { get; set; } = new List<DartDependency>();
}
