using Assessment3.Server.Domain.Common;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.UserEvents;

namespace Assessment3.Server.Application.UserEvents.Commons;

public record UserEventResults(Event Event, User User);
public record UserEventResult(UserEvent Event);