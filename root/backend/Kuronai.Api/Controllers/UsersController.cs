using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.Mappers;
using Kuronai.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kuronai.Api.Controllers;

public class UsersController : MealGenBaseController
{
    readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> SearchUsers([FromQuery] string search)
    {
        var users = (await _userService.FindUsers(search)).ToList();

        var requestingUser = users.Find(e => e.Id == FirebaseUserId);
        if (requestingUser is not null)
            users.Remove(requestingUser);

        return Ok(users.Select(u => u.ToUserResponse()));
    }

    [HttpGet]
    public async Task<ActionResult<UserDetailsResponse>> LoadUserDetails()
    {
        var user = await _userService.LoadUser(FirebaseUserId);
        if (user is null) return NotFound();

        return Ok(user);
    }
}
