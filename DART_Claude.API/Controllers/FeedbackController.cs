using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/feedback")]
public sealed class FeedbackController : ControllerBase
{
    private readonly GetApplicationFeedbackHandler _getApplicationFeedbackHandler;

    public FeedbackController(GetApplicationFeedbackHandler getApplicationFeedbackHandler)
    {
        _getApplicationFeedbackHandler = getApplicationFeedbackHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeedbackAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationFeedbackHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }
}
