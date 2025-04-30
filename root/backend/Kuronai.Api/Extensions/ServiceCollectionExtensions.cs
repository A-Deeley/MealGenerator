using FirebaseAdmin;
using Kuronai.Api.EFCore;
using Kuronai.Api.Options;
using Kuronai.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFirebase(this IServiceCollection services, Action<FirebaseOptions> configure)
    {
        FirebaseOptions options = new();
        configure(options);

        FirebaseApp.Create(options.ToAppOptions());

        return services;
    }

    public static IServiceCollection AddDepedencies(this IServiceCollection services)
    {
        services
            .AddScoped<IFirebaseService, FirebaseService>()
            .AddScoped<IHouseholdService, HouseholdService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserInviteService, UserInviteService>()
            .AddScoped<IRecipeTagService, RecipeTagService>()
            .AddScoped<IRecipeConfigurationService, RecipeConfiguratorService>()
            .AddSingleton<IRecipeGeneratorService, RecipeGeneratorService>();

        return services;
    }

    public static IServiceCollection AddEntityFramework(this IServiceCollection services)
    {
        services.AddDbContext<MealGenDbContext>();

        return services;
    }

    public static IServiceCollection AddEntityFrameworkDev(this IServiceCollection services)
    {
        services
            .AddDbContext<MealGenDbContext>(options =>
            {
                options.UseSqlite("DataSource=C:\\sqlite\\test.db");
            });

        return services;
    }
}
