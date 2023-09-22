namespace Assessment3.Server.Domain.Authentication.Common;
using ErrorOr;
public static partial class UserErrors
{
    public static Error DuplicateEmail =>
        Error.Conflict(code: "User.DuplicateEmail", description: "Email already in use.");
}