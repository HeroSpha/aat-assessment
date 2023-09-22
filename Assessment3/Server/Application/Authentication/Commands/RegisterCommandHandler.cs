using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Common.Authentication;
using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Helpers;
using Assessment3.Server.Domain.Authentication.Common;
using Assessment3.Server.Domain.Authentication.Errors;
using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;
using ErrorOr;
using MediatR;

namespace Assessment3.Server.Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IRepository<User> _userRepository;
    private readonly IUserRepository _repository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IRepository<User> userRepository, IUserRepository repository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _repository = repository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //check if the user already exists (Validation)
        var user = await _repository.GetUserByEmail(command.Email);
        if (!Object.Equals(user, null))
        {
            return  UserErrors.DuplicateEmail;
        }
        var (salt, hashedPassword) = PasswordHelper.HashPassword(command.Password);

        user = User.Create(
            command.FirstName, 
            command.LastName,
            command.Email,
            hashedPassword,
            salt,
            command.Role);

        await _userRepository.InsertAsync(user);
        
        //create token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
        //return default;
    }
}