using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;
using Kuronai.Api.Mappers;
using Kuronai.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kuronai.Api.Controllers;

public class HouseholdsController : MealGenBaseController
{
    readonly IHouseholdService _householdService;
    readonly IUserInviteService _userInviteService;

    public HouseholdsController(IHouseholdService service, IUserInviteService userInviteService)
    {
        _householdService = service;
        _userInviteService = userInviteService;
    }

    // GET: api/Households
    [HttpGet]
    public async Task<ActionResult<HouseholdListViewModel>> GetHouseholds()
    {
        var households = await _householdService.GetHouseholds(FirebaseUserId);

        var vm = new HouseholdListViewModel()
        {
            OwnerOf = households.Where(e => e.OwnerId == FirebaseUserId).Select(HouseholdMapper.ToHouseholdResponse),
            MemberOf = households.Where(e => e.OwnerId != FirebaseUserId).Select(HouseholdMapper.ToHouseholdResponse)
        };

        return Ok(vm);
    }

    // GET: api/Households/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HouseholdViewResponse>> GetHousehold(int id)
    {
        var household = await _householdService.GetHousehold(id, FirebaseUserId);

        if (household is null)
        {
            return NotFound();
        }

        household.IsOwner = household.OwnerId == FirebaseUserId;

        return Ok(household);
    }

    // PUT: api/Households/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHousehold(int id, PutHouseholdRequest request)
    {
        if (id != request.Id) return BadRequest();
        try
        {
            var household = await _householdService.UpdateHousehold(request, FirebaseUserId);
            return Ok(household.ToHouseholdResponse());
        }
        catch (ResourceNotFoundException<Household>) { return NotFound(); }
        catch (UnauthorizedUserException) { return Forbid(); }
    }

    // POST: api/Households
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<HouseholdResponse>> PostHousehold(PostHouseholdRequest request)
    {
        var household = await _householdService.CreateHousehold(request, FirebaseUserId);
        return CreatedAtAction("GetHousehold", new { id = household.Id }, HouseholdMapper.ToHouseholdResponse(household));
    }

    // DELETE: api/Households/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHousehold(int id)
    {
        try
        {
            await _householdService.DeleteHousehold(id, FirebaseUserId);
            return NoContent();
        }
        catch (ResourceNotFoundException<Household>) { return NotFound(); }
        catch (UnauthorizedUserException) { return Forbid(); }
    }

    [HttpPost("{id}/Recipes")]
    public async Task<IActionResult> AddRecipeToHousehold(int id, [FromBody] PostRecipeRequest request)
    {
        try
        {
            await _householdService.AddHouseholdRecipe(id, request, FirebaseUserId);
            return Ok();
        }
        catch (ResourceNotFoundException<Household>) { return NotFound("Household does not exist!"); }
        catch (UnauthorizedUserException) { return Forbid(); }
    }

    [HttpDelete("{id}/Recipes/{recipeId}")]
    public async Task<IActionResult> RemoveRecipeFromHousehold(int id, int recipeId)
    {
        try
        {
            await _householdService.DeleteHouseholdRecipe(id, recipeId, FirebaseUserId);
            return NoContent();
        }
        catch (ResourceNotFoundException<Household>) { return NotFound("Household does not exist!"); }
        catch (ResourceNotFoundException<Recipe>) { return NotFound("Recipe does not exist!"); }
        catch (UnauthorizedUserException) { return Forbid(); }
    }

    [HttpPost("{id}/User")]
    public async Task<IActionResult> InviteUserToHousehold(int id, [FromBody]UserResponse user)
    {
        await _householdService.InviteMember(id, user.Id);

        return Ok();
    }

    [HttpPost("{id}/Invite")]
    public async Task<IActionResult> UpdateConfirmation(int id, [FromBody]ChangeInviteRequest request)
    {
        if (request.Accepted)
        {
            await _householdService.AddMember(id, FirebaseUserId);
            await _userInviteService.DeleteInvite(request.InviteId);
        }
        else
        {
            await _userInviteService.DeleteInvite(request.InviteId);
        }

        return Ok();
    }
}
