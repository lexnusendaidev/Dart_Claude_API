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

    public async Task<IApplicationListItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        VwDartApplicationList? entity = await _context.VwDartApplicationLists
            .FirstOrDefaultAsync(application => application.AppId == id, cancellationToken);
        return entity;
    }

    // Reads from the raw DartApplications table, not the view, since GetDetailByIdAsync's callers
    // need the raw foreign key ids (AppTypeId, CriticalityId, ...) to prefill an edit form, not the
    // view's resolved display-name strings.
    public async Task<IApplicationDetail?> GetDetailByIdAsync(int id, CancellationToken cancellationToken)
    {
        DartApplication? entity = await _context.DartApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(application => application.AppId == id, cancellationToken);
        return entity;
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

    public async Task UpdateAsync(UpdatedApplication application, CancellationToken cancellationToken)
    {
        DartApplication? entity = await _context.DartApplications
            .FindAsync(new object[] { application.Id }, cancellationToken);
        if (entity is not null)
        {
            entity.AppName = application.Name;
            entity.AppCurrentVersion = application.CurrentVersion;
            entity.AppDescription = application.Description;
            entity.AppType = application.AppTypeId;
            entity.AppCriticalityId = application.CriticalityId;
            entity.AppPrimDeveloperEmpId = application.PrimaryDeveloperEmpId;
            entity.AppSecondaryDeveloperEmpId = application.SecondaryDeveloperEmpId;
            entity.AppAnalystEmpId = application.AnalystEmpId;
            entity.AppSdlcPhaseId = application.SdlcPhaseId;
            entity.AppSdlcCheckDate = application.SdlcCheckDate;
            entity.AppFriendlyName = application.FriendlyName;
            entity.AppAllowFeedback = application.AllowFeedback;
            entity.UpdateDate = DateTime.UtcNow;
            entity.UpdateBy = application.UpdatedByEmpId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
