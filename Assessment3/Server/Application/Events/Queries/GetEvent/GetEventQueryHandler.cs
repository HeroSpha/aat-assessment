using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.Events.Queries.GetEvents;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.Events.Errors;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Queries.GetEvent;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, ErrorOr<EventResult>>
{
    private readonly IRepository<Event> _eventRepository;

    public GetEventQueryHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<EventResult>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
        if (existingEvent is null)
        {
            return  EventErrors.NotFoundError;
        }

        return new EventResult(existingEvent);
    }
}