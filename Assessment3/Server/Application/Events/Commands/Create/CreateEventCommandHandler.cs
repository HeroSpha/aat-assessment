using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Domain.Events;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Commands.Create;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ErrorOr<EventResult>>
{
    private readonly IRepository<Event> _eventRepository;

    public CreateEventCommandHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<EventResult>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = Event.Create(
            request.Title,
            request.Description,
            request.Image,
            request.Date,
            request.Venue,
            request.Seats);
         await _eventRepository.InsertAsync(newEvent);
         return new EventResult(newEvent);
    }
}