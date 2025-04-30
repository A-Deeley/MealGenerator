using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Access;

public class PostLoginRequest
{
    [Required]
    public string IdToken { get; set; } = null!;
}
