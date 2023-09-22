using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Common.Authentication;
using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Helpers;
using Assessment3.Server.Domain.Authentication.Errors;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(command.Email);
        if (Object.Equals(user, null))
        {
            return AuthenticationErrors.InvalidCredentials;
        }
        
        if (!PasswordHelper.VerifyPassword(command.Password, user.Salt, user.PasswordHash))
        {
            return AuthenticationErrors.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}