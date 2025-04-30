using Kuronai.Api.EFCore;

namespace Kuronai.Api.Exceptions;

/// <summary>
/// Exception that is thrown whenever an attempt is made to access a resource that does not exist.
/// </summary>
public class ResourceNotFoundException<T> : MealGenException
{
    static readonly string ERROR_MESSAGE = $"{nameof(T)} resource not found.";

    public ResourceNotFoundException() : base(ERROR_MESSAGE)
    {
    }

    public ResourceNotFoundException(Exception? innerException) : base(ERROR_MESSAGE, innerException)
    {
    }
}
