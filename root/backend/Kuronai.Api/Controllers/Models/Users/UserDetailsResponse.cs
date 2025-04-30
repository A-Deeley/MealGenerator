using Kuronai.Api.Controllers.Models.HouseholdUserInvite;

namespace Kuronai.Api.Controllers.Models.Users;

public class UserDetailsResponse
{
    public string Id { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public IEnumerable<HouseholdUserInviteResponse> PendingInvites { get; set; } = [];
}
