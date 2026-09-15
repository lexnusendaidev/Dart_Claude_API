using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/feedback/{feedbackId}/files")]
public sealed class FeedbackFilesController : ControllerBase
{
    private readonly GetFeedbackFilesHandler _getFeedbackFilesHandler;

    public FeedbackFilesController(GetFeedbackFilesHandler getFeedbackFilesHandler)
    {
        _getFeedbackFilesHandler = getFeedbackFilesHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetFilesAsync(int feedbackId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getFeedbackFilesHandler.HandleAsync(feedbackId, cancellationToken)).ToActionResult();
        return result;
    }
}
