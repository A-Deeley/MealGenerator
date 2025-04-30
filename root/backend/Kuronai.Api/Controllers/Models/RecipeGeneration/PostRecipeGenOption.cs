namespace Kuronai.Api.Controllers.Models.RecipeGeneration;

public class PostRecipeGenOption
{
    public int HouseholdId { get; set; }
    public int RecipeId { get; set; }
    public int MaxWeeklyOccurence { get; set; }
    public int MinWeeklyOccurence { get; set; }
    public int MinReoccurenceDelayWeeks { get; set; }
}
