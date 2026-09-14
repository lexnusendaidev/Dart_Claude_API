namespace DART_Claude.API.Handlers;

public sealed class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public int StatusCode { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorMessage { get; init; }
    public T? Value { get; init; }

    public static ServiceResult<T> Success(T value, int statusCode = StatusCodes.Status200OK)
    {
        return new ServiceResult<T> { IsSuccess = true, StatusCode = statusCode, Value = value };
    }

    public static ServiceResult<T> Failure(int statusCode, string errorCode, string errorMessage)
    {
        return new ServiceResult<T> { IsSuccess = false, StatusCode = statusCode, ErrorCode = errorCode, ErrorMessage = errorMessage };
    }
}
