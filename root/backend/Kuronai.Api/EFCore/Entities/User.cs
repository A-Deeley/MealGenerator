using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuronai.Api.EFCore.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public ICollection<Household> OwnerOf { get; set; } = [];
    public ICollection<Household> MemberOf { get; set; } = [];
    public ICollection<HouseholdUserInvite> Invites { get; set; } = [];
}
