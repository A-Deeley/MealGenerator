using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Tests;

public abstract class MealGenTestBase
{
    protected SqliteConnection _connection;
    protected DbContextOptions<MealGenDbContext> _options;
    protected readonly List<string> _testUsers = ["testuser_0", "testuser_1", "testuser_2"];

    public MealGenTestBase()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<MealGenDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new MealGenDbContext(_options);

        if (context.Database.EnsureCreated())
        {
            var owner0 = new User
            {
                Id = _testUsers[0],
                Nickname = "test_user 0",
                Email = "test_user_0@test.com"
            };

            var owner1 = new User
            {
                Id = _testUsers[1],
                Nickname = "test_user 1",
                Email = "test_user_1@test.com"
            };

            var owner2 = new User
            {
                Id = _testUsers[2],
                Nickname = "test_user 2",
                Email = "test_user_2@test.com"
            };

            var recipe0 = new Recipe
            {
                Title = "Cookies"
            };

            var household0 = new Household
            {
                Name = "testhouse_0",
                Owner = owner0,
                Members = [owner1]

            };

            var household1 = new Household
            {
                Name = "testhouse_1",
                Owner = owner1,
                Members = [owner0],
                Recipes = [recipe0]
            };

            var household2 = new Household
            {
                Name = "testhouse_2",
                Owner = owner2
            };

            context.AddRange([recipe0]);
            context.AddRange([owner0, owner1, owner2]);
            context.AddRange([household0, household1, household2]);
            context.SaveChanges();
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
