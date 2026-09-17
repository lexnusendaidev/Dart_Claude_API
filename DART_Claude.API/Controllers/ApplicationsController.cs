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
    private readonly GetApplicationByIdHandler _getApplicationByIdHandler;
    private readonly CreateApplicationHandler _createApplicationHandler;

    public ApplicationsController(
        GetApplicationsHandler getApplicationsHandler,
        GetApplicationByIdHandler getApplicationByIdHandler,
        CreateApplicationHandler createApplicationHandler)
    {
        _getApplicationsHandler = getApplicationsHandler;
        _getApplicationByIdHandler = getApplicationByIdHandler;
        _createApplicationHandler = createApplicationHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationsAsync(CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationsHandler.HandleAsync(cancellationToken)).ToActionResult();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationByIdAsync(int id, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationByIdHandler.HandleAsync(id, cancellationToken)).ToActionResult();
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
