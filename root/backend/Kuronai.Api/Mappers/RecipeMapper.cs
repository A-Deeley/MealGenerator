using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Mappers;

internal static class RecipeMapper
{
    internal static Recipe ToRecipe(this PostRecipeRequest request)
    {
        var recipe = new Recipe()
        {
            Title = request.Title
        };

        return recipe;
    }

    internal static Recipe ToRecipe(this PutRecipeRequest request) 
    {
        var recipe = new Recipe()
        {
            Id = request.Id,
            Title = request.Title
        };

        return recipe;
    }

    internal static RecipeResponse ToResponse(this Recipe recipe)
    {
        var recipeResponse = new RecipeResponse()
        {
            Id = recipe.Id,
            Title = recipe.Title,
        };

        return recipeResponse;
    }
}
