using Kuronai.Api.Exceptions;

namespace Kuronai.Api.Services;

public interface IFirebaseUserService
{

}

public interface IFirebaseUserService<T> : IFirebaseUserService
    where T : IFirebaseUserService
{
    public T WithUserId(string firebaseUserId);
}

public abstract class FirebaseUserService
{
    protected string? _requestUserId;

    protected void RequireFirebaseUserId()
    {
        if (string.IsNullOrWhiteSpace(_requestUserId)) throw new UnauthorizedUserException();
    }
}
