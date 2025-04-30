namespace Kuronai.Api.EFCore.Entities;

public class HouseholdTagOptions : EntityBase
{
    public int HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
    public int TagId { get; set; }
    public RecipeTag RecipeTag { get; set; } = null!;
    public int MinReoccurenceDelayWeeks { get; set; }
    public int MinWeeklyOccurence { get; set; }
    public int MaxWeeklyOccurence { get; set; }
}
