using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Households;

public class PutHouseholdRequest
{
    [Required]
    public int Id { get; set; }

    [Required, MinLength(1)]
    public string Name { get; set; } = null!;

    [Required, MinLength(1)]
    public string OwnerId { get; set; } = null!;
}
