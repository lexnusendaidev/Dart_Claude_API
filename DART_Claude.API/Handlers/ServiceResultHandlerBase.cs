using DART_Claude.Common.Constants;
using DART_Claude.Common.Exceptions;

namespace DART_Claude.API.Handlers;

public abstract class ServiceResultHandlerBase
{
    private readonly ILogger _logger;

    protected ServiceResultHandlerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected async Task<ServiceResult<T>> ExecuteAsync<T>(
        Func<Task<T>> operation,
        string handlerName,
        int successStatusCode = StatusCodes.Status200OK)
    {
        _logger.LogInformation("{Handler} starting", handlerName);
        try
        {
            T value = await operation();
            _logger.LogInformation("{Handler} completed", handlerName);
            ServiceResult<T> result = ServiceResult<T>.Success(value, successStatusCode);
            return result;
        }
        catch (EntityNotFoundException exception)
        {
            _logger.LogWarning(exception, "{Handler} entity not found", handlerName);
            ServiceResult<T> result = ServiceResult<T>.Failure(StatusCodes.Status404NotFound, ErrorCodes.EntityNotFound, exception.Message);
            return result;
        }
        catch (BusinessRuleException exception)
        {
            _logger.LogWarning(exception, "{Handler} business rule violation", handlerName);
            ServiceResult<T> result = ServiceResult<T>.Failure(StatusCodes.Status400BadRequest, ErrorCodes.BusinessRuleViolation, exception.Message);
            return result;
        }
    }
}
