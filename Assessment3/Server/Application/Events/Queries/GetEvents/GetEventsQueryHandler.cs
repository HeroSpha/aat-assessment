using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.Events.Queries.GetEvents;
using Assessment3.Server.Domain.Events;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Queries.GetEvents;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, ErrorOr<EventsResult>>
{
    private readonly IRepository<Event> _eventRepository;

    public GetEventsQueryHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<EventsResult>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAsync(
            x => x.Seats > 0 && 
                 x.Date > DateTime.Now);
        return new EventsResult(events);
    }
}