using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuronai.Api.EFCore.Entities;

public class Household : EntityBase
{
    public string Name { get; set; } = "My home";
    public User Owner { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public ICollection<User> Members { get; set; } = [];
    public ICollection<Recipe> Recipes { get; set; } = [];
    public ICollection<HouseholdUserInvite> Invites { get; set; } = [];
}