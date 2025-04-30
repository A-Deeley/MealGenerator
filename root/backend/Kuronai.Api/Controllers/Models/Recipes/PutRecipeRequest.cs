using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Recipes;

public class PutRecipeRequest
{
    [Required]
    public int Id { get; set; }

    [Required, MinLength(1)]
    public string Title { get; set; } = null!;
}
