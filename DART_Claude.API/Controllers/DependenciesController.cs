using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using DART_Claude.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/dependencies")]
public sealed class DependenciesController : ControllerBase
{
    private readonly GetApplicationDependenciesHandler _getApplicationDependenciesHandler;
    private readonly CreateDependencyHandler _createDependencyHandler;
    private readonly DeleteDependencyHandler _deleteDependencyHandler;

    public DependenciesController(
        GetApplicationDependenciesHandler getApplicationDependenciesHandler,
        CreateDependencyHandler createDependencyHandler,
        DeleteDependencyHandler deleteDependencyHandler)
    {
        _getApplicationDependenciesHandler = getApplicationDependenciesHandler;
        _createDependencyHandler = createDependencyHandler;
        _deleteDependencyHandler = deleteDependencyHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetDependenciesAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationDependenciesHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDependencyAsync(
        int applicationId,
        [FromBody] CreateDependencyRequest request,
        CancellationToken cancellationToken)
    {
        IActionResult result = (await _createDependencyHandler.HandleAsync(applicationId, request, cancellationToken)).ToActionResult();
        return result;
    }

    [HttpDelete("{dependOnAppId}")]
    public async Task<IActionResult> DeleteDependencyAsync(
        int applicationId,
        int dependOnAppId,
        CancellationToken cancellationToken)
    {
        IActionResult result = (await _deleteDependencyHandler.HandleAsync(applicationId, dependOnAppId, cancellationToken)).ToActionResult();
        return result;
    }
}
