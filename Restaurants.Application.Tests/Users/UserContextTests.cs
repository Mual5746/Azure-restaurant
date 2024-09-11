
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        //arrange 
        var dateOfBirth = new DateOnly(1990, 1,1);
       // var httpContextAccessorMock = new Mock<HttpContextAccessor>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@test.com"),
            new Claim(ClaimTypes.Role, UserRoles.Admin),
            new Claim(ClaimTypes.Role, UserRoles.User),
            new ("Nationality", "Svensk"),
            new ("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))      
        }; 
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var UserContext = new UserContext(httpContextAccessorMock.Object);

        //Act
        var CurrentUser = UserContext.GetCurrentUser();

        // asset 

        CurrentUser.Should().NotBeNull();
        CurrentUser?.Id.Should().Be("1");
        CurrentUser?.Email.Should().Be("test@test.com");
        CurrentUser?.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        CurrentUser?.Nationality.Should().Be("Svensk");
        CurrentUser?.DateOfBirth.Should().Be(dateOfBirth);
    }

  [Fact]
  public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
    {
        //ARRANGE

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null!);

        //var UserContext = new UserContext(httpContextAccessor.Object);
        var userContext = new UserContext(httpContextAccessorMock.Object);

       //Act

        Action action= () => userContext.GetCurrentUser();

       //asset

       action.Should().Throw<InvalidOperationException>()
       .WithMessage("User context is not present");
    }
}