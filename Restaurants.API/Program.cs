
using Restaurants.Infrastructure.Extentions;
using Restaurants.Application.Extensions;
using Serilog;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//builder.Configuration.GetConnectionString("RestaurantsDb"); //passa denna till AddInfrastructure
//add dbcontext 
//builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString);
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
//await seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
