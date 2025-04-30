using FirebaseAdmin.Auth;
using Kuronai.Api.Controllers.Models.Access;
using Kuronai.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Kuronai.Api.Controllers;

public class AccessController : MealGenBaseController
{
    readonly IFirebaseService _firebase;

    public AccessController(IFirebaseService firebase)
    {
        _firebase = firebase;
    }

    [HttpPost("SignUp/Email"), AllowAnonymous]
    public async Task<ActionResult> SignUp([FromBody] PostSignUpEmailRequest request)
    {
        await _firebase.CreateNewUser(request);
        return Ok();
    }

    [HttpGet]
    public ActionResult CheckToken() => Ok();

    [HttpPost("Login"), AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] PostLoginRequest request)
    {
        // Set session expiration to 5 days.
        var options = new SessionCookieOptions()
        {
            ExpiresIn = TimeSpan.FromDays(5),
        };

        try
        {
            // Create the session cookie. This will also verify the ID token in the process.
            // The session cookie will have the same claims as the ID token.
            var rawJwtToken = await FirebaseAuth.DefaultInstance
                .CreateSessionCookieAsync(request.IdToken, options);



            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(rawJwtToken);
            var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity);
            return Ok();
        }
        catch (FirebaseAuthException)
        {
            return Unauthorized("Failed to create a session cookie");
        }
    }

    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}
