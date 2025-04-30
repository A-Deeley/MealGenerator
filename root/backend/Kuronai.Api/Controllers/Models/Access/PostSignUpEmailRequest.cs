using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kuronai.Api.Controllers.Models.Access;

public class PostSignUpEmailRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? Nickname { get; set; }
}
