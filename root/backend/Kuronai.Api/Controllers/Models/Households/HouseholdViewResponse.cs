using Kuronai.Api.Controllers.Models.HouseholdUserInvite;
using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Controllers.Models.Households;

public class HouseholdViewResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public bool IsOwner { get; set; }
    public IEnumerable<RecipeResponse> Recipes { get; set; } = [];
    public IEnumerable<HouseholdUserInviteResponse> PendingInvites { get; set; } = [];
    public IEnumerable<UserResponse> Members { get; set; } = [];
}
