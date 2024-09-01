
using Restaurants.Infrastructure.Extentions;
using Restaurants.Application.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();




//builder.Configuration.GetConnectionString("RestaurantsDb"); //passa denna till AddInfrastructure
//add dbcontext 
//builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration)

    /*
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
        .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
    */ 

    /* Flyssta till appsetings

    "Serilog": {
        "MinimumLevel": {
            "Override": {
                "Microsoft": "Warning",
                "Microsoft.EntityFrameworkCore": "Information"
            }
            },
            "WriteTo": [
            {
                "Name": "Console",
                "Args": {
                "outputTemplate": "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}"
                }
            },
            {
                "Name": "File",
                "Args": {
                "path": "Logs/Restaurant-Api-.log",
                "rollingInterval": "Day",
                "rollOnFileSizeLimit": true,
                "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
                }
            }
            ]
        }
    */
);

var app = builder.Build();

var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
//await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
