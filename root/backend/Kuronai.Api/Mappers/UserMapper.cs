using Kuronai.Api.Controllers.Models.Users;
using Kuronai.Api.EFCore.Entities;

namespace Kuronai.Api.Mappers;

internal static class UserMapper
{
    internal static UserResponse ToUserResponse(this User user)
    {
        return new()
        {
            Id = user.Id,
            Nickname = user.Nickname,
        };
    }
}
