using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.Controllers.Models.HouseholdUserInvite;
using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.Controllers.Models.RecipeTags;
using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;
using Kuronai.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Services;

public interface IHouseholdService
{
    /// <summary>
    /// Gets all <see cref="Household"/> for the requested user.
    /// </summary>
    /// <param name="userId">The unique user id.</param>
    /// <returns>A list of households the user owns or is member of.</returns>
    public Task<IEnumerable<Household>> GetHouseholds(string userId);

    /// <summary>
    /// Gets a <see cref="Household"/> for the user by id.
    /// </summary>
    /// <param name="id">The id of the household.</param>
    /// <param name="userId">The unique user id.</param>
    /// <returns>The <see cref="Household"/> if it exists and the user is owner or member of, or <see langword="null"/> if no objects match the id and userId.</returns>
    public Task<HouseholdViewResponse?> GetHousehold(int id, string userId);

    /// <summary>
    /// Updates an existing household with new values.
    /// </summary>
    /// <param name="dto">The household to edit.</param>
    /// <param name="userId">The unique user id.</param>
    /// <exception cref="UnauthorizedUserException" />
    /// <exception cref="ResourceNotFoundException" />
    /// <returns>The updated <see cref="Household"/> instance.</returns>
    public Task<Household> UpdateHousehold(PutHouseholdRequest dto, string userId);

    /// <summary>
    /// Creates a new household with the requesting user as the owner.
    /// </summary>
    /// <param name="dto">The household information.</param>
    /// <param name="userId">The unique user id.</param>
    /// <returns>The created <see cref="Household"/> instance.</returns>
    public Task<Household> CreateHousehold(PostHouseholdRequest dto, string userId);

    /// <summary>
    /// Deletes a household the user is an owner of.
    /// </summary>
    /// <param name="id">The household's ID.</param>
    /// <param name="userId">The unique user id.</param>
    /// <exception cref="UnauthorizedUserException" />
    /// <exception cref="ResourceNotFoundException" />
    public Task DeleteHousehold(int id, string userId);

    /// <summary>
    /// Returns an array of <see cref="Recipe"/> associated with the household.
    /// </summary>
    /// <param name="householdId">The household id to query.</param>
    /// <param name="userId">The unique user id making.</param>
    /// <exception cref="UnauthorizedUserException" />
    /// <exception cref="ResourceNotFoundException" />
    /// <returns>A list of <see cref="Recipe"/>.</returns>
    public Task<IEnumerable<Recipe>> GetHouseholdRecipes(int householdId, string userId);

    /// <summary>
    /// Adds a <see cref="Recipe"/> to the household.
    /// </summary>
    /// <param name="householdId">The household id to query.</param>
    /// <param name="recipe">The <see cref="Recipe"/> to add.</param>
    /// <param name="userId">The unique user id.</param>
    /// <returns></returns>
    public Task AddHouseholdRecipe(int householdId, PostRecipeRequest recipe, string userId);

    /// <summary>
    /// Deletes a <see cref="Recipe"/> from the household.
    /// </summary>
    /// <param name="householdId">The household id to query.</param>
    /// <param name="recipeId">The recipe id to remove.</param>
    /// <param name="userId">The unique user id.</param>
    /// <exception cref="UnauthorizedUserException" />
    /// <exception cref="ResourceNotFoundException" />
    /// <returns></returns>
    public Task DeleteHouseholdRecipe(int householdId, int recipeId,  string userId);

    /// <summary>
    /// Adds a <see cref="User"/> to the household.
    /// </summary>
    /// <param name="householdId">The household id to query.</param>
    /// <param name="userIdToAdd">The unique user id to add.</param>
    /// <returns></returns>
    public Task<HouseholdUserInvite> InviteMember(int householdId, string userIdToAdd);

    /// <summary>
    /// Removes a <see cref="User"/> from the household.
    /// </summary>
    /// <param name="householdId">The household id to query.</param>
    /// <param name="userIdToRemove">The unique user id to remove.</param>
    /// <returns></returns>
    public Task DeleteHouseholdMember(int householdId, string userIdToRemove);
    public Task AddMember(int id, string userId);
}

public class HouseholdService : IHouseholdService
{
    readonly MealGenDbContext _db;

    public HouseholdService(MealGenDbContext context)
    {
        _db = context;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Household>> GetHouseholds(string userId)
    {
        var households = await _db
            .Households
            .AsNoTracking()
            .Where(e => e.OwnerId == userId || e.Members.Any(m => m.Id == userId))
            .ToListAsync();

        return households;
    }

    /// <inheritdoc/>
    public async Task<HouseholdViewResponse?> GetHousehold(int id, string userId)
    {
        return await _db
            .Households
            .AsNoTracking()
            .Where(e => e.Id == id && (e.OwnerId == userId || e.Members.Any(m => m.Id == userId)))
            .Select(e => new HouseholdViewResponse()
            {
                Id = e.Id,
                Name = e.Name,
                OwnerId = e.OwnerId,
                Members = e.Members.Select(m => new UserResponse()
                {
                    Id = m.Id,
                    Nickname = m.Nickname,
                }),
                Recipes = e.Recipes.Select(r => new RecipeResponse()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Tags = r.RecipeTags.Select(t => new RecipeTagResponse() { Id = t.Id, Name = t.Name, Colour = t.Colour })
                }),
                PendingInvites = e.Invites.Select(i => new HouseholdUserInviteResponse()
                {
                    User = new() { Id = i.User.Id, Nickname = i.User.Nickname }
                })
            })
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<Household> UpdateHousehold(PutHouseholdRequest dto, string userId)
    {
        Household? entity = await _db.Households.FindAsync(dto.Id);

        if (entity is null) throw new ResourceNotFoundException<Household>();

        if (entity.OwnerId != userId) throw new UnauthorizedUserException();

        entity.Apply(dto);
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc/>
    public async Task<Household> CreateHousehold(PostHouseholdRequest dto, string userId)
    {
        Household entity = dto.ToHousehold(userId);
        _db.Add(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc/>
    public async Task DeleteHousehold(int id, string userId)
    {
        Household? entity = await _db.Households.FindAsync(id);

        if (entity is null) throw new ResourceNotFoundException<Household>();

        if (entity.OwnerId != userId) throw new UnauthorizedUserException();

        _db.Remove(entity);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Recipe>> GetHouseholdRecipes(int householdId, string userId)
    {
        Household? entity = await _db
            .Households
            .AsNoTracking()
            .Include(e => e.Recipes)
            .Where(e => e.Id == householdId)
            .FirstOrDefaultAsync();

        if (entity is null) throw new ResourceNotFoundException<Household>();
        if (entity.OwnerId != userId && !entity.Members.Any(m => m.Id == userId)) throw new UnauthorizedUserException();

        return entity.Recipes;
    }

    /// <inheritdoc/>
    public async Task AddHouseholdRecipe(int householdId, PostRecipeRequest request, string userId)
    {
        Household? entity = await _db
            .Households
            .Include(e => e.Members)
            .Where(e => e.Id == householdId)
            .FirstOrDefaultAsync();

        if (entity is null) throw new ResourceNotFoundException<Household>();

        if (entity.OwnerId != userId && !entity.Members.Any(m => m.Id == userId)) throw new UnauthorizedUserException();

        var tags = await _db
            .RecipeTags
            .Where(e => request.Tags.Contains(e.Id))
            .ToListAsync();

        Recipe recipe = new()
        {
            Title = request.Title,
            RecipeTags = tags
        };

        entity.Recipes.Add(recipe);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteHouseholdRecipe(int householdId, int recipeId, string userId)
    {
        Household? entity = await _db
            .Households
            .Include(e => e.Recipes)
            .Where(e => e.Id == householdId)
            .FirstOrDefaultAsync();

        if (entity is null) throw new ResourceNotFoundException<Household>();
        if (entity.OwnerId != userId) throw new UnauthorizedUserException();

        var recipe = entity.Recipes.FirstOrDefault(e => e.Id == recipeId);
        if (recipe is null) throw new ResourceNotFoundException<Recipe>();

        entity.Recipes.Remove(recipe);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<HouseholdUserInvite> InviteMember(int householdId, string userIdToAdd)
    {
        Household? entity = await _db
            .Households
            .Include(e => e.Members)
            .Include(e => e.Invites.Where(i => i.UserId == userIdToAdd))
            .Where(e => e.Id == householdId)
            .FirstOrDefaultAsync();

        if (entity is null) throw new ResourceNotFoundException<Household>();
        if (entity.OwnerId == userIdToAdd) throw new OperationInvalidException("Cannot add owner as member");
        if (entity.Members.Any(e => e.Id == userIdToAdd)) throw new OperationInvalidException("User is already a member");
        if (entity.Invites.Count > 0) throw new OperationInvalidException("User already has a pending invite");

        var user = _db.Users.Where(e => e.Id == userIdToAdd).FirstOrDefault();
        if (user is null) throw new ResourceNotFoundException<User>();

        HouseholdUserInvite invite = new() { UserId = userIdToAdd, HouseholdId = entity.Id };
        _db.Add(invite);
        await _db.SaveChangesAsync();

        return invite;
    }

    /// <inheritdoc/>
    public async Task DeleteHouseholdMember(int householdId, string userIdToRemove)
    {
        Household? entity = await _db
            .Households
            .Include(e => e.Members)
            .Where(e => e.Id == householdId)
            .FirstOrDefaultAsync();

        if (entity is null) throw new ResourceNotFoundException<Household>();

        if (entity.OwnerId == userIdToRemove) throw new OperationInvalidException("Cannot remove owner");

        var userToRemove = entity.Members.FirstOrDefault(e => e.Id == userIdToRemove);
        if (userToRemove is null) throw new OperationInvalidException("User is not a member");

        entity.Members.Remove(userToRemove);
        await _db.SaveChangesAsync();
    }

    public async Task AddMember(int id, string userId)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user is null) throw new ResourceNotFoundException<User>();

        var household = await _db.Households.FindAsync(id);
        if (household is null) throw new ResourceNotFoundException<Household>();

        household.Members.Add(user);
        await _db.SaveChangesAsync();
    }
}
