using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Mappers;

internal static class HouseholdMapper
{
    internal static Household ToHousehold(this PostHouseholdRequest request, string userId)
    {
        var recipe = new Household()
        {
            Name = request.Name,
            OwnerId = userId,
        };

        return recipe;
    }

    internal static HouseholdResponse ToHouseholdResponse(this Household household)
    {
        var householdResponse = new HouseholdResponse()
        {
            Id = household.Id,
            Name = household.Name,
        };

        return householdResponse;
    }

    internal static Household Apply(this Household entity, PutHouseholdRequest dto)
    {
        entity.Name = dto.Name;
        entity.OwnerId = dto.OwnerId;

        return entity;
    }
}
