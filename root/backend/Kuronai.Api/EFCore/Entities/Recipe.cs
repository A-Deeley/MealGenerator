using System.ComponentModel.DataAnnotations.Schema;

namespace Kuronai.Api.EFCore.Entities;

public class Recipe : EntityBase
{
    public string Title { get; set; } = null!;
    public ICollection<Household> Households { get; set; } = [];
    public ICollection<RecipeTag> RecipeTags { get; set; } = [];
}
