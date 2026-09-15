using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class FeedbackFileRepository : IFeedbackFileRepository
{
    private readonly DartClaudeContext _context;

    public FeedbackFileRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IDartFeedbackFile>> GetByFeedbackIdAsync(int feedbackId, CancellationToken cancellationToken)
    {
        List<DartFeedbackFile> entities = await _context.DartFeedbackFiles
            .Where(file => file.FfDartFeedbackId == feedbackId)
            .ToListAsync(cancellationToken);
        List<IDartFeedbackFile> files = entities.Cast<IDartFeedbackFile>().ToList();
        return files;
    }
}
