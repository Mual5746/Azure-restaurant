using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService( IRestaurantsRepository restaurantsRepository,
ILogger<RestaurantsService> logger,
IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating a new restaurant");

        var restaurant = mapper.Map<Restaurant>(dto);

        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation( "Geting all Restaurants" );
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>( restaurants);
        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation( $"Geting a Restaurant by {id}" );
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
       // var restaurantDto = RestaurantDto.FromEntity(restaurant);
       var restaurantDto = mapper.Map<RestaurantDto?>( restaurant);
        return restaurantDto;
    }
    
}