using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/dependencies")]
public sealed class DependenciesController : ControllerBase
{
    private readonly GetApplicationDependenciesHandler _getApplicationDependenciesHandler;

    public DependenciesController(GetApplicationDependenciesHandler getApplicationDependenciesHandler)
    {
        _getApplicationDependenciesHandler = getApplicationDependenciesHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetDependenciesAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationDependenciesHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }
}
