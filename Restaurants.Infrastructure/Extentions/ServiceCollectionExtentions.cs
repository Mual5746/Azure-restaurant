using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Repository;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extentions; 

public static class ServiceCollectionExtentions 
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>( options => 
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
        .AddRoles<IdentityRole>()
        .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
        .AddEntityFrameworkStores<RestaurantsDbContext>();
        
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository >();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "Svensk", "Norsk"));
    }
}
