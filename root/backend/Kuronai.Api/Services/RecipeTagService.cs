using Kuronai.Api.Controllers.Models.RecipeTags;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Services;

public interface IRecipeTagService
{
    public Task<IEnumerable<RecipeTagResponse>> GetRecipeTags();

    public Task<RecipeTagResponse?> GetRecipeTag(int id);

    public Task<RecipeTagResponse> UpdateTag(PostRecipeTagRequest recipeTag);
    public Task DeleteTag(int id);
}

public class RecipeTagService : IRecipeTagService
{
    readonly MealGenDbContext _db;

    public RecipeTagService(MealGenDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task DeleteTag(int id)
    {
        var tag = await _db.RecipeTags.FindAsync(id);
        if (tag is null) throw new ResourceNotFoundException<RecipeTag>();

        _db.Remove(tag);
        await _db.SaveChangesAsync();
    }

    public async Task<RecipeTagResponse?> GetRecipeTag(int id)
    {
        var tag = await _db
            .RecipeTags
            .Where(e => e.Id == id)
            .Select(e => new RecipeTagResponse()
            {
                Id = e.Id,
                Name = e.Name,
                Colour = e.Colour
            })
            .FirstOrDefaultAsync();

        return tag;
    }

    public async Task<IEnumerable<RecipeTagResponse>> GetRecipeTags()
    {
        var tag = await _db
            .RecipeTags
            .Select(e => new RecipeTagResponse()
            {
                Id = e.Id,
                Name = e.Name,
                Colour = e.Colour
            })
            .ToListAsync();

        return tag;
    }

    public async Task<RecipeTagResponse> UpdateTag(PostRecipeTagRequest recipeTag)
    {
        RecipeTag? dbTag;
        if (recipeTag.Id is null)
        {
            dbTag = new()
            {
                Name = recipeTag.Name,
                Colour = recipeTag.Colour,
            };

            _db.RecipeTags.Add(dbTag);
        }
        else
        {
            dbTag = await _db.RecipeTags.FindAsync(recipeTag.Id);
            if (dbTag is null) throw new ResourceNotFoundException<RecipeTag>();
            dbTag.Name = recipeTag.Name;
            dbTag.Colour = recipeTag.Colour;
        }
        await _db.SaveChangesAsync();

        return new()
        {
            Id = dbTag.Id,
            Name = dbTag.Name,
            Colour = dbTag.Colour,
        };
    }
}
