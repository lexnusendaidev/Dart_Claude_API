using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications")]
public sealed class ApplicationsController : ControllerBase
{
    private readonly GetApplicationsHandler _getApplicationsHandler;

    public ApplicationsController(GetApplicationsHandler getApplicationsHandler)
    {
        _getApplicationsHandler = getApplicationsHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationsAsync(CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationsHandler.HandleAsync(cancellationToken)).ToActionResult();
        return result;
    }
}
