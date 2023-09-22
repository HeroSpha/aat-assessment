
using Assessment3.Server.Application.Authentication.Commands;
using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Authentication.Queries.Login;
using Assessment3.Server.Domain.Authentication.Errors;
using Assessment3.Shared.Models;
using Assessment3.Shared.Models.Authentication;
using MapsterMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// ReSharper disable once CheckNamespace
namespace Assessment3.Server.Controllers;
[Route("authentication")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(
            command);
        return authResult.Match(
            auth => Ok(_mapper.Map<AuthenticationResponse>(auth)),
            Problem);
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var loginQuery = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(
            loginQuery);
        if (authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        return authResult.Match(
            auth => Ok(_mapper.Map<AuthenticationResponse>(auth)),
            Problem);
    }
}