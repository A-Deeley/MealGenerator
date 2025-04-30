using FirebaseAdmin.Auth;
using Kuronai.Api.Controllers.Models.Access;

namespace Kuronai.Api.Mappers;

internal static class FirebaseMapper
{
    internal static UserRecordArgs ToUserRecordArgs(this PostSignUpEmailRequest request) => new()
    {
        Disabled = false,
        DisplayName = request.Nickname ?? "",
        Email = request.Email,
        Password = request.Password,
    };
}
