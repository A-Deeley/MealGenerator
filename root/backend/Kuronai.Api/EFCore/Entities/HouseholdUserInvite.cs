namespace Kuronai.Api.EFCore.Entities;

public class HouseholdUserInvite : EntityBase
{
    public Household Household { get; set; } = null!;
    public int HouseholdId { get; set; }
    public User User { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
