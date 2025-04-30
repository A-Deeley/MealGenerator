namespace Kuronai.Api.Exceptions;

/// <summary>
/// Exception thrown when a requested action cannot be performed.
/// </summary>
public class OperationInvalidException : MealGenException
{
    public OperationInvalidException(string message) : base(message)
    {
    }
}
