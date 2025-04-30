using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Recipes;

public class PostRecipeRequest
{
    [Required, MinLength(1)]
    public string Title { get; set; } = null!;

    public IEnumerable<int> Tags { get; set; } = [];
}
