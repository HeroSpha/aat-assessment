using Assessment3.Server.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;