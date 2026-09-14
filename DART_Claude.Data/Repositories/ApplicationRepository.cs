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

    public async Task<List<IDartApplication>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<DartApplication> entities = await _context.DartApplications.ToListAsync(cancellationToken);
        List<IDartApplication> applications = entities.Cast<IDartApplication>().ToList();
        return applications;
    }
}
