

using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserDetailsCommand;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly? DateOfBith {get; set;}
    public string? Nationality {get; set;}

}