using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;

namespace Kuronai.Api.Services;

public interface IUserInviteService
{
    public Task DeleteInvite(int id);
}

public class UserInviteService : IUserInviteService
{
    readonly MealGenDbContext _db;

    public UserInviteService(MealGenDbContext context)
    {
        _db = context;
    }

    public async Task DeleteInvite(int id)
    {
        var invite = await _db.HouseholdUserInvites.FindAsync(id);
        if (invite is null)
            throw new ResourceNotFoundException<HouseholdUserInvite>();

        _db.Remove(invite);
        await _db.SaveChangesAsync();
    }
}
