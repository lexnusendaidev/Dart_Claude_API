using DART_Claude.API.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DART_Claude.API.Extensions;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
    {
        ObjectResult actionResult = new(result) { StatusCode = result.StatusCode };
        return actionResult;
    }
}
