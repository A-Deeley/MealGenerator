using System.Runtime.Serialization;

namespace Kuronai.Api.Exceptions;

/// <summary>
/// Wraps all application-specific exceptions into a single base type.
/// <para>
/// Example
/// <code>
/// try
/// {
///     _myService.DoSomething();
/// }
/// catch(<see cref="MealGenException"/> e) 
/// {
///     // Here you would know it was an application-specific exception and not any .NET exception
/// }
/// </code>
/// </para>
/// </summary>
public class MealGenException : Exception
{
    public MealGenException()
    {
    }

    public MealGenException(string? message) : base(message)
    {
    }

    public MealGenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
