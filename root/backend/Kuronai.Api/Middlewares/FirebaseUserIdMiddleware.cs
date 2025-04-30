using System.Text;

namespace Kuronai.Api.Middlewares;

public class FirebaseUserIdMiddleware
{
    readonly RequestDelegate _next;
    readonly List<string> IGNORED_ROUTES = ["/Access/Login", "/Access/SignUp/Email"];

    public FirebaseUserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "OPTIONS" || IGNORED_ROUTES.Contains(context.Request.Path))
        {
            await _next(context);
        }
        else
        {
            var userId = context.User.FindFirst("user_id")?.Value;

            if (userId is null)
            {
                context.Response.StatusCode = 400;
                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("User id missing from claims!"));
                return;
            }

            await _next(context);
        }
    }
}
