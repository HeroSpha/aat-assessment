namespace Assessment3.Server.Domain.UserEvents.Errors;
using ErrorOr;
public class UserEventErrors
{
    public static Error NotFoundError =>
        Error.Conflict(code: "UserEvent.NotFoundError", description: "User event cannot be found.");
    public static Error RegisteredError =>
        Error.Conflict(code: "UserEvent.RegisteredError", description: "User already registered for this event.");
}