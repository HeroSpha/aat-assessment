using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Events.Commom;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Commands.Delete;

public record EventDeleteCommand(
    Guid Id) : IRequest<ErrorOr<bool>>;