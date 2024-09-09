
using MediatR;

namespace Restaurants.Application.Users.Commands.AssignRole;

public class AssignUserRoleCommand : IRequest
{
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set;} = default!;
}