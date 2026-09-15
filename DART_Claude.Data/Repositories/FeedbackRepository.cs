using DART_Claude.Data.ContextModels;
using DART_Claude.Models;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.Repositories;

public sealed class FeedbackRepository : IFeedbackRepository
{
    private readonly DartClaudeContext _context;

    public FeedbackRepository(DartClaudeContext context)
    {
        _context = context;
    }

    public async Task<List<IDartFeedback>> GetByApplicationIdAsync(int applicationId, CancellationToken cancellationToken)
    {
        List<DartFeedback> entities = await _context.DartFeedbacks
            .Where(feedback => feedback.FbAppId == applicationId)
            .ToListAsync(cancellationToken);
        List<IDartFeedback> feedback = entities.Cast<IDartFeedback>().ToList();
        return feedback;
    }
}
