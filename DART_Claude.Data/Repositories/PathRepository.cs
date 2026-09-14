using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class PathRepository : IPathRepository
{
    private readonly DartClaudeContext _context;

    public PathRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IPathListItem>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<VwDartPathList> entities = await _context.VwDartPathLists
            .Where(path => path.ApplicationId == applicationId)
            .ToListAsync(cancellationToken);
        List<IPathListItem> paths = entities.Cast<IPathListItem>().ToList();
        return paths;
    }
}
