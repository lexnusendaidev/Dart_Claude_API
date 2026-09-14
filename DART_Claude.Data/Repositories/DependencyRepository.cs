using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class DependencyRepository : IDependencyRepository
{
    private readonly DartClaudeContext _context;

    public DependencyRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IDependencyListItem>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<VwDartDependencyList> entities = await _context.VwDartDependencyLists
            .Where(dependency => dependency.DependAppId == applicationId)
            .ToListAsync(cancellationToken);
        List<IDependencyListItem> dependencies = entities.Cast<IDependencyListItem>().ToList();
        return dependencies;
    }
}
