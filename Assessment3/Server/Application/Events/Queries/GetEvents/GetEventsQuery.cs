using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Events.Commom;
using MediatR;
using ErrorOr;
namespace Assessment3.Server.Application.Events.Queries.GetEvents;

public record GetEventsQuery() 
    : IRequest<ErrorOr<EventsResult>>;