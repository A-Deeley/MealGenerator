using FirebaseAdmin.Auth;
using Kuronai.Api.Controllers.Models.Access;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Exceptions;
using Kuronai.Api.Mappers;
namespace Kuronai.Api.Services;

public interface IFirebaseService
{
    Task<string> CreateNewUser(PostSignUpEmailRequest dto);
}

public class FirebaseService : IFirebaseService
{
    readonly MealGenDbContext _db;

    public FirebaseService(MealGenDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<string> CreateNewUser(PostSignUpEmailRequest dto)
    {
        var userRecordArgs = dto.ToUserRecordArgs();
        var response = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

        if (response is null) throw new MealGenException("Unable to create user");

        var user = new User
        {
            Id = response.Uid,
            Email = response.Email,
            Nickname = response.DisplayName
        };
        _db.Add(user);
        await _db.SaveChangesAsync();

        return response.Uid;
    }
}

