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

    public async Task<bool> ExistsAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken)
    {
        bool exists = await _context.DartDependencies
            .AnyAsync(dependency => dependency.DependAppId == applicationId && dependency.DependOnId == dependOnApplicationId, cancellationToken);
        return exists;
    }

    public async Task CreateAsync(NewDependency dependency, CancellationToken cancellationToken)
    {
        DartDependency entity = new()
        {
            DependAppId = dependency.ApplicationId,
            DependOnId = dependency.DependOnApplicationId,
        };

        _context.DartDependencies.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int applicationId, int dependOnApplicationId, CancellationToken cancellationToken)
    {
        // Key order must match DartDependency's HasKey(e => new { e.DependAppId, e.DependOnId })
        // declaration -- swapping these two args would silently look up the wrong pair.
        DartDependency? entity = await _context.DartDependencies
            .FindAsync(new object[] { applicationId, dependOnApplicationId }, cancellationToken);
        if (entity is not null)
        {
            _context.DartDependencies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
