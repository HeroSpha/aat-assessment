using Assessment3.Server.Domain.Events;

namespace Assessment3.Server.Application.Events.Commom;

public record EventsResult(IEnumerable<Event> Events);