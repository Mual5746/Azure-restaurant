
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
        [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange  
           // Skapar en mock för ILogger<CreateRestaurantCommandHandler> som används för loggning i handlern.
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        // Skapar en mock för IMapper som används för att mappa kommandon till domänmodeller.
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();

         // Skapar en ny restaurangentitet som kommer att mappas från kommandot.
        var restaurant = new Restaurant();
      
       // Ställer in mapperMock så att den returnerar restaurangen när Map anropas med ett CreateRestaurantCommand.
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);
       
        // Skapar en mock för IRetaurantsRepository för att hantera restaurangrelaterade databasoperationer.
         var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);


        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, 
            mapperMock.Object, 
            restaurantRepositoryMock.Object,
            userContextMock.Object);

        // act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
    }
}