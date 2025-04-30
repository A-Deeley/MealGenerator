using Kuronai.Api.Controllers.Models.RecipeGeneration;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Services;

public interface IRecipeConfigurationService : IFirebaseUserService<IRecipeConfigurationService>
{
    public Task<IEnumerable<GetRecipeGenOptions>> GetRecipeOptions(int householdId);
    public Task CreateRecipeOption(PostRecipeGenOption postRecipeGenOption);
}
public class RecipeConfiguratorService : FirebaseUserService, IRecipeConfigurationService
{
    readonly MealGenDbContext _db;

    public RecipeConfiguratorService(MealGenDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GetRecipeGenOptions>> GetRecipeOptions(int householdId)
    {
        RequireFirebaseUserId();

        return await _db.HouseholdRecipeOptions
            .Where(e => e.HouseholdId == householdId && e.Household.OwnerId == _requestUserId)
            .Select(e => new GetRecipeGenOptions()
            {
                Id = e.Id,
                HouseholdId = e.HouseholdId,
                Recipe = new()
                {
                    Id = e.Recipe.Id,
                    Title = e.Recipe.Title
                },
                MaxWeeklyOccurence = e.MaxWeeklyOccurence,
                MinReoccurenceDelayWeeks = e.MinReoccurenceDelayWeeks,
                MinWeeklyOccurence = e.MinWeeklyOccurence
            }).ToListAsync();
    }

    public async Task CreateRecipeOption(PostRecipeGenOption postRecipeGenOption)
    {
        RequireFirebaseUserId();

        bool isOwner = await _db.Households.Where(e => e.OwnerId == _requestUserId && e.Id == postRecipeGenOption.HouseholdId).AnyAsync();

        if (!isOwner)
            return;

        HouseholdRecipeOptions recipeOpt = new()
        {
            MaxWeeklyOccurence = postRecipeGenOption.MaxWeeklyOccurence,
            MinWeeklyOccurence = postRecipeGenOption.MinWeeklyOccurence,
            MinReoccurenceDelayWeeks = postRecipeGenOption.MinReoccurenceDelayWeeks,
            RecipeId = postRecipeGenOption.RecipeId,
            HouseholdId = postRecipeGenOption.HouseholdId,
        };

        _db.Add(recipeOpt);
        await _db.SaveChangesAsync();
    }

    IRecipeConfigurationService IFirebaseUserService<IRecipeConfigurationService>.WithUserId(string firebaseUserId)
    {
        _requestUserId = firebaseUserId;
        return this;
    }
}
