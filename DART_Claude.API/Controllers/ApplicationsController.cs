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
    private readonly GetApplicationDetailHandler _getApplicationDetailHandler;
    private readonly CreateApplicationHandler _createApplicationHandler;
    private readonly UpdateApplicationHandler _updateApplicationHandler;

    public ApplicationsController(
        GetApplicationsHandler getApplicationsHandler,
        GetApplicationByIdHandler getApplicationByIdHandler,
        GetApplicationDetailHandler getApplicationDetailHandler,
        CreateApplicationHandler createApplicationHandler,
        UpdateApplicationHandler updateApplicationHandler)
    {
        _getApplicationsHandler = getApplicationsHandler;
        _getApplicationByIdHandler = getApplicationByIdHandler;
        _getApplicationDetailHandler = getApplicationDetailHandler;
        _createApplicationHandler = createApplicationHandler;
        _updateApplicationHandler = updateApplicationHandler;
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

    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetApplicationDetailAsync(int id, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationDetailHandler.HandleAsync(id, cancellationToken)).ToActionResult();
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplicationAsync(
        int id,
        [FromBody] UpdateApplicationRequest request,
        CancellationToken cancellationToken)
    {
        IActionResult result = (await _updateApplicationHandler.HandleAsync(id, request, cancellationToken)).ToActionResult();
        return result;
    }
}
