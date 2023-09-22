namespace Assessment3.Server.Domain.Authentication.Errors;
using ErrorOr;
public class AuthenticationErrors
{
    public static Error InvalidCredentials =>
        Error.Conflict(code: "Authentication.InvalidCredentials", description: "Invalid credentials");
}