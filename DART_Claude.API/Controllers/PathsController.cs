using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/paths")]
public sealed class PathsController : ControllerBase
{
    private readonly GetApplicationPathsHandler _getApplicationPathsHandler;

    public PathsController(GetApplicationPathsHandler getApplicationPathsHandler)
    {
        _getApplicationPathsHandler = getApplicationPathsHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetPathsAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationPathsHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }
}
