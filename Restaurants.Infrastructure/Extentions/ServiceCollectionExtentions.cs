using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extentions; 

public static class ServiceCollectionExtentions 
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>( options => options.UseSqlServer(connectionString));

        
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    }
}
