using Kuronai.Api.Services;

namespace Kuronai.Tests;

public class UserServiceTests : MealGenTestBase
{
    UserService CreateService() => new UserService(new(_options));

    [Fact]
    public async Task FindUsers_ReturnsResults_WhenPartialMatch()
    {
        var service = CreateService();

        // Should find our test users, which have nicknames test_user XX
        var userResults = await service.FindUsers("st_us");

        Assert.NotEmpty(userResults);
    }
}
