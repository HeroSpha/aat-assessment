using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.UserEvents.Commons;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.UserEvents.Commands.Create;

public record CreateUserEventCommand(
    Guid UserId,
    Guid EventId) : IRequest<ErrorOr<UserEventResult>>;