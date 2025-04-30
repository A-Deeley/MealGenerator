namespace Kuronai.Api.EFCore.Entities;

public class HouseholdRecipeOptions : EntityBase
{
    public int HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int MinReoccurenceDelayWeeks { get; set; }
    public int MinWeeklyOccurence { get; set; }
    public int MaxWeeklyOccurence { get; set; }
}
