using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using DART_Claude.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/paths")]
public sealed class PathsController : ControllerBase
{
    private readonly GetApplicationPathsHandler _getApplicationPathsHandler;
    private readonly CreatePathHandler _createPathHandler;

    public PathsController(GetApplicationPathsHandler getApplicationPathsHandler, CreatePathHandler createPathHandler)
    {
        _getApplicationPathsHandler = getApplicationPathsHandler;
        _createPathHandler = createPathHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetPathsAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationPathsHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePathAsync(
        int applicationId,
        [FromBody] CreatePathRequest request,
        CancellationToken cancellationToken)
    {
        IActionResult result = (await _createPathHandler.HandleAsync(applicationId, request, cancellationToken)).ToActionResult();
        return result;
    }
}
