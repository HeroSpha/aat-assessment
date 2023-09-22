using Assessment3.Server.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Authentication.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password, 
    string Role) : IRequest<ErrorOr<AuthenticationResult>>;