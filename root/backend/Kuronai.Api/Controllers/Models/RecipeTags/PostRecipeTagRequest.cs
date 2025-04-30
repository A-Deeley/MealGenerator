using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.RecipeTags;

public class PostRecipeTagRequest
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Colour { get; set; } = null!;
}
