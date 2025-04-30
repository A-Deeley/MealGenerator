using Kuronai.Api.Controllers.Models.RecipeTags;
using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Recipes;

public class RecipeResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public IEnumerable<RecipeTagResponse> Tags { get; set; } = [];
}
