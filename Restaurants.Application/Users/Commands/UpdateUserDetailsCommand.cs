

using MediatR;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly? DateOfBith {get; set;}
    public string? Nationality {get; set;}

}