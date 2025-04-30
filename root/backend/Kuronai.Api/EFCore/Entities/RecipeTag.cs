namespace Kuronai.Api.EFCore.Entities;

public class RecipeTag : EntityBase
{
    public required string Name { get; set; }
    public required string Colour { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; } = [];
}
