using Kuronai.Api.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.EFCore;

public class MealGenDbContext : DbContext
{
    public MealGenDbContext(DbContextOptions<MealGenDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.OwnerOf)
            .WithOne(e => e.Owner)
            .HasForeignKey(e => e.OwnerId);

        modelBuilder.Entity<User>()
            .HasMany(e => e.MemberOf)
            .WithMany(e => e.Members);

        modelBuilder.Entity<Household>()
            .HasIndex(e => e.OwnerId)
            .HasDatabaseName("IX_OwnerId");
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Household> Households { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<HouseholdUserInvite> HouseholdUserInvites { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<HouseholdTagOptions> HouseholdTagOptions { get; set; }
    public DbSet<HouseholdRecipeOptions> HouseholdRecipeOptions { get; set; }
}
