using ErrorOr;

namespace Assessment3.Server.Domain.Events.Errors;

public static class EventErrors
{
    public static Error NotFoundError =>
        Error.Conflict(code: "Events.NotFoundError", description: "Event cannot be found.");
}