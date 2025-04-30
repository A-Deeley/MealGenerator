using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kuronai.Api.Middlewares;

namespace Kuronai.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public abstract class MealGenBaseController : ControllerBase
{
    /// <summary>
    /// Retrieves the firebase user id from the user_id claim. 
    /// This value should always be available since the <see cref="FirebaseUserIdMiddleware"/> validates that the incoming
    /// request has this claim.
    /// </summary>
    protected string FirebaseUserId { get => HttpContext.User.FindFirst("user_id")!.Value; }

}
