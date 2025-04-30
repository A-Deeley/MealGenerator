using Kuronai.Api.Controllers.Models.Households;
using Kuronai.Api.Controllers.Models.HouseholdUserInvite;
using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Services;

public interface IUserService
{
    public Task<IEnumerable<User>> FindUsers(string search);

    public Task<UserDetailsResponse?> LoadUser(string userId);
}

public class UserService : IUserService
{
    readonly MealGenDbContext _db;

    public UserService(MealGenDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<IEnumerable<User>> FindUsers(string search)
    {
        return await _db
            .Users
            .Where(e => EF.Functions.Like(e.Nickname, $"%{search}%") || EF.Functions.Like(e.Id, $"%{search}%"))
            .ToListAsync();
    }

    public async Task<UserDetailsResponse?> LoadUser(string userId)
    {
        return await _db
            .Users
            .AsNoTracking()
            .Where(e => e.Id == userId)
            .Select(e => new UserDetailsResponse()
            {
                Id = e.Id,
                Nickname = e.Nickname,
                PendingInvites = e.Invites.Select(i => new HouseholdUserInviteResponse()
                {
                    Id = i.Id,
                    Household = new HouseholdResponse()
                    {
                        Id = i.Household.Id,
                        Name = i.Household.Name
                    },
                })
            })
            .FirstOrDefaultAsync();
    }
}
