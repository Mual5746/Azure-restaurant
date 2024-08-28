
using Restaurants.Infrastructure.Extentions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();




//builder.Configuration.GetConnectionString("RestaurantsDb"); //passa denna till AddInfrastructure
//add dbcontext 
//builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
//await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
