using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Exceptions;

/// <summary>
/// Exception that is thrown whenever a <see cref="User"/> tries to perform an action on a resource they are not permitted to.
/// </summary>
public class UnauthorizedUserException : MealGenException
{
    const string ERROR_MESSAGE = "You are not authorized to perform this action.";

    public UnauthorizedUserException() : base(ERROR_MESSAGE)
    {
    }

    public UnauthorizedUserException(Exception? innerException) : base(ERROR_MESSAGE, innerException)
    {
    }
}
