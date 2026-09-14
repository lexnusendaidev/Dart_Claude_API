namespace DART_Claude.Common.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
}
