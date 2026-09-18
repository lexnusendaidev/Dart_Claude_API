using DART_Claude.Models;

namespace DART_Claude.Data.ContextModels;

public partial class DartApplication : IApplicationDetail
{
    int IApplicationDetail.Id => AppId;
    string IApplicationDetail.Name => AppName;
    decimal IApplicationDetail.CurrentVersion => AppCurrentVersion;
    string? IApplicationDetail.Description => AppDescription;
    int IApplicationDetail.AppTypeId => AppType;
    int IApplicationDetail.CriticalityId => AppCriticalityId;
    short IApplicationDetail.PrimaryDeveloperEmpId => AppPrimDeveloperEmpId;
    short? IApplicationDetail.SecondaryDeveloperEmpId => AppSecondaryDeveloperEmpId;
    short? IApplicationDetail.AnalystEmpId => AppAnalystEmpId;
    short IApplicationDetail.SdlcPhaseId => AppSdlcPhaseId;
    DateTime IApplicationDetail.SdlcCheckDate => AppSdlcCheckDate;
    string IApplicationDetail.FriendlyName => AppFriendlyName;
    bool IApplicationDetail.AllowFeedback => AppAllowFeedback;
}
