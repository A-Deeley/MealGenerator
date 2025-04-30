using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.Controllers.Models.Users;

namespace Kuronai.Api.Controllers.Models.HouseholdUserInvite;

public class HouseholdUserInviteResponse
{
    public int Id { get; set; }
    public HouseholdResponse Household { get; set; }
    public UserResponse User { get; set; }
}
