using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Events.Commom;
using MediatR;
using ErrorOr;
namespace Assessment3.Server.Application.Events.Queries.GetEvent;

public record GetEventQuery(Guid Id) 
    : IRequest<ErrorOr<EventResult>>;