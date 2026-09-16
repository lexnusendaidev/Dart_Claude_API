using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class ApplicationRepository : IApplicationRepository
{
    private readonly DartClaudeContext _context;

    public ApplicationRepository(DartClaudeContext context)
    {
        _context = context;
    }

    // Reads from vw_DART_ApplicationList (scaffolded as VwDartApplicationList) rather than the raw
    // DartApplications table, so the list already carries resolved Criticality/AppType/SDLC phase/
    // developer names instead of just foreign key ids.
    public async Task<List<IApplicationListItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<VwDartApplicationList> entities = await _context.VwDartApplicationLists.ToListAsync(cancellationToken);
        List<IApplicationListItem> applications = entities.Cast<IApplicationListItem>().ToList();
        return applications;
    }

    public async Task<int> CreateAsync(NewApplication application, CancellationToken cancellationToken)
    {
        DartApplication entity = new()
        {
            AppName = application.Name,
            AppCurrentVersion = application.CurrentVersion,
            AppDescription = application.Description,
            AppType = application.AppTypeId,
            AppCriticalityId = application.CriticalityId,
            AppPrimDeveloperEmpId = application.PrimaryDeveloperEmpId,
            AppSecondaryDeveloperEmpId = application.SecondaryDeveloperEmpId,
            AppAnalystEmpId = application.AnalystEmpId,
            AppSdlcPhaseId = application.SdlcPhaseId,
            AppSdlcCheckDate = application.SdlcCheckDate,
            AppFriendlyName = application.FriendlyName,
            AppAllowFeedback = application.AllowFeedback,
            CreateDate = DateTime.UtcNow,
            CreateBy = application.CreatedByEmpId,
        };

        _context.DartApplications.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.AppId;
    }
}
