using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;
using Kuronai.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Tests;

public class HouseholdServiceTests : MealGenTestBase
{
    HouseholdService CreateService() => new(new(_options));

    [Fact]
    public async Task GetHouseholds_ShouldReturnHouseholds_UserIsOwnerAndMemberOf()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];

        IEnumerable<Household> households = await service.GetHouseholds(userId);

        Assert.Equal(2, households.Count());
    }

    [Fact]
    public async Task GetHousehold_ShouldReturnNull_IfNotExists()
    {
        HouseholdService service = CreateService();
        int nonExistantId = int.MaxValue;

        HouseholdViewResponse? household = await service.GetHousehold(nonExistantId, _testUsers[0]);

        Assert.Null(household);
    }

    [Fact]
    public async Task GetHousehold_ShouldReturnNull_IfExistsButUserNotOwnerOrMember()
    {
        HouseholdService service = CreateService();
        string memberUserId = _testUsers[0];
        int nonMemberHouseholdId = 3;

        HouseholdViewResponse? household = await service.GetHousehold(nonMemberHouseholdId, memberUserId);

        Assert.Null(household);
    }

    [Fact]
    public async Task UpdateHousehold_ShouldThrowResourceNotFound_IfEntityDoesNotExist()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        PutHouseholdRequest updateRequest = new() { Id = 5, Name = "Something", OwnerId = userId };

        await Assert.ThrowsAsync<ResourceNotFoundException<Household>>(() => service.UpdateHousehold(updateRequest, userId));
    }

    [Fact]
    public async Task UpdateHousehold_ShouldThrowUnauthorizedUser_IfUserNotOwner()
    {
        HouseholdService service = CreateService();
        string nonOwnerUserId = _testUsers[0];
        PutHouseholdRequest updateRequest = new()
        {
            Id = 3,
            Name = "Something",
            OwnerId = _testUsers[2]
        };

        await Assert.ThrowsAsync<UnauthorizedUserException>(() => service.UpdateHousehold(updateRequest, nonOwnerUserId));
    }

    [Fact]
    public async Task DeleteHousehold_ShouldThrowResourceNotFound_IfEntityDoesNotExist()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        int nonExistantId = int.MaxValue;

        await Assert.ThrowsAsync<ResourceNotFoundException<Household>>(() => service.DeleteHousehold(nonExistantId, userId));
    }

    [Fact]
    public async Task DeleteHousehold_ShouldThrowUnauthorizedUser_IfUserNotOwner()
    {
        HouseholdService service = CreateService();
        string nonOwnerUserId = _testUsers[0];
        int owner2HouseholdId = 3;

        await Assert.ThrowsAsync<UnauthorizedUserException>(() => service.DeleteHousehold(owner2HouseholdId, nonOwnerUserId));
    }

    [Fact]
    public async Task GetHouseholdRecipes_ShouldThrowUnauthorizedUser_IfUserNotOwnerOrMember()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[2];

        await Assert.ThrowsAsync<UnauthorizedUserException>(() => service.GetHouseholdRecipes(1, userId));
    }

    [Fact]
    public async Task GetHouseholdRecipes_ShouldThrowResourceNotFound_IfHouseholdNotExists()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[1];
        int nonExistantId = int.MaxValue;

        await Assert.ThrowsAsync<ResourceNotFoundException<Household>>(() => service.GetHouseholdRecipes(nonExistantId, userId));
    }

    [Fact]
    public async Task DeleteRecipe_ShouldThrowUnauthorizedUser_IfUserNotOwner()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        int recipeId = 1;
        int nonOwnerHouseholdId = 2;

        await Assert.ThrowsAsync<UnauthorizedUserException>(() => service.DeleteHouseholdRecipe(nonOwnerHouseholdId, recipeId, userId));
    }

    [Fact]
    public async Task DeleteRecipe_ShouldThrowResourceNotFound_IfHouseholdNotFound()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        int recipeId = 1;
        int nonExistantId = int.MaxValue;

        await Assert.ThrowsAsync<ResourceNotFoundException<Household>>(() => service.DeleteHouseholdRecipe(nonExistantId, recipeId, userId));
    }

    [Fact]
    public async Task DeleteRecipe_ShouldThrowResourceNotFound_IfRecipeNotFound()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        int nonExistantId = int.MaxValue;
        int householdId = 1;

        await Assert.ThrowsAsync<ResourceNotFoundException<Recipe>>(() => service.DeleteHouseholdRecipe(householdId, nonExistantId, userId));
    }

    [Fact]
    public async Task AddRecipe_ShouldThrowUnauthorizedUser_IfUserNotOwner()
    {
        HouseholdService service = CreateService();
        string userId = _testUsers[0];
        PostRecipeRequest recipe = new();
        int nonOwnerHousehold = 3;

        await Assert.ThrowsAsync<UnauthorizedUserException>(() => service.AddHouseholdRecipe(nonOwnerHousehold, recipe, userId));
    }
}
