using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using DART_Claude.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications")]
public sealed class ApplicationsController : ControllerBase
{
    private readonly GetApplicationsHandler _getApplicationsHandler;
    private readonly CreateApplicationHandler _createApplicationHandler;

    public ApplicationsController(
        GetApplicationsHandler getApplicationsHandler,
        CreateApplicationHandler createApplicationHandler)
    {
        _getApplicationsHandler = getApplicationsHandler;
        _createApplicationHandler = createApplicationHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationsAsync(CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationsHandler.HandleAsync(cancellationToken)).ToActionResult();
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreateApplicationAsync(
        [FromBody] CreateApplicationRequest request,
        CancellationToken cancellationToken)
    {
        IActionResult result = (await _createApplicationHandler.HandleAsync(request, cancellationToken)).ToActionResult();
        return result;
    }
}
