using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/telemetry")]
public sealed class TelemetryController : ControllerBase
{
    private readonly GetApplicationTelemetryHandler _getApplicationTelemetryHandler;

    public TelemetryController(GetApplicationTelemetryHandler getApplicationTelemetryHandler)
    {
        _getApplicationTelemetryHandler = getApplicationTelemetryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetTelemetryAsync(int applicationId, CancellationToken cancellationToken)
    {
        IActionResult result = (await _getApplicationTelemetryHandler.HandleAsync(applicationId, cancellationToken)).ToActionResult();
        return result;
    }
}
