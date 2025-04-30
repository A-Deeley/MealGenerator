using Kuronai.Api.Controllers.Models.RecipeGeneration;
using Kuronai.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kuronai.Api.Controllers;

public class RecipeConfigController : MealGenBaseController
{
    readonly IRecipeConfigurationService _recipeConfigService;

    public RecipeConfigController(IRecipeConfigurationService recipeConfigService)
    {
        _recipeConfigService = recipeConfigService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRecipeGenOptions>>> GetRecipeOptions(int householdId)
    {
        var recipeOpts = await _recipeConfigService
            .WithUserId(FirebaseUserId)
            .GetRecipeOptions(householdId);
        return Ok(recipeOpts);
    }

    [HttpPost]
    public async Task<ActionResult> CreateRecipeOption([FromBody] PostRecipeGenOption request)
    {
        await _recipeConfigService
            .WithUserId(FirebaseUserId)
            .CreateRecipeOption(request);
        return Ok();
    }
}
