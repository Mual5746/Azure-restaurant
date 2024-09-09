using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{       
    /*
    Denna klass är central i applikationens dataåtkomstlager. Den hanterar hur Entity Framework Core 
    interagerar med databasen genom att tillhandahålla tabellrepresentationer (via DbSet) och 
    konfigurera relationerna mellan entiteter (via OnModelCreating). Med denna konfiguration kan
     applikationen utföra operationer på restauranger och deras rätter utan att behöva skriva rå 
     SQL-kod.
    */
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) 
    : IdentityDbContext<User>(options) {
    /*
     Konstruktorn tar en DbContextOptions<RestaurantsDbContext> 
    parameter, som innehåller konfigurationen för databaskopplingen (t.ex. vilken databas som ska 
    användas, anslutningssträngar etc.). Den vidarebefordrar denna parameter till basklassen DbContext
     via base(options).
    */

    //Tabler i databasen 
        internal DbSet<Restaurant> Restaurants { get; set;}
        internal DbSet<Dish> Dishes { get; set;}

        //Denna metod används för att konfigurera modellen som motsvarar databasen. Här kan du ange regler och relationer mellan entiteter.
         protected override void OnModelCreating (ModelBuilder modelBuilder)
         {
            base.OnModelCreating (modelBuilder);

            //Detta säger åt Entity Framework att Restaurant-entiteten har en ägd entitet Address. 
            //Det innebär att Address inte existerar som en separat tabell utan är en del av Restaurant-tabellen som en inbäddad egenskap.
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

            /*
            Här konfigureras en en-till-många-relation mellan Restaurant och Dishes. Detta betyder att 
            en restaurang kan ha många rätter (HasMany), och varje rätt (Dish) är associerad med exakt 
            en restaurang (WithOne()). HasForeignKey(d => d.RestaurantId) anger att det är RestaurantId
             i Dish-tabellen som fungerar som den främmande nyckeln.
            */
            modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(r => r.RestaurantId);

            modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
         }
    }
}


