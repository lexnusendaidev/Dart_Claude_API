using DART_Claude.API.Extensions;
using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Controllers;

[ApiController]
[Route("api/lookups")]
public sealed class LookupsController : ControllerBase
{
    private readonly GetLookupsHandler _getLookupsHandler;

    public LookupsController(GetLookupsHandler getLookupsHandler)
    {
        _getLookupsHandler = getLookupsHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetLookupsAsync(CancellationToken cancellationToken)
    {
        IActionResult result = (await _getLookupsHandler.HandleAsync(cancellationToken)).ToActionResult();
        return result;
    }
}
