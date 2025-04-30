using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.Controllers.Models.RecipeTags;
using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Controllers.Models.RecipeGeneration;

public class GetRecipeGenOptions
{
    public int Id { get; set; }
    public int HouseholdId { get; set; }
    public RecipeResponse Recipe { get; set; }
    public int MinReoccurenceDelayWeeks { get; set; }
    public int MinWeeklyOccurence { get; set; }
    public int MaxWeeklyOccurence { get; set; }
}
