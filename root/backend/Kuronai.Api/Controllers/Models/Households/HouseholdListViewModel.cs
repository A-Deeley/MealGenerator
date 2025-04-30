namespace Kuronai.Api.Controllers.Models.Households;

public class HouseholdListViewModel
{
    public IEnumerable<HouseholdResponse> OwnerOf { get; set; } = [];
    public IEnumerable<HouseholdResponse> MemberOf { get; set; } = [];
}
