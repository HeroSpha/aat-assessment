using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.Events.Errors;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Commands.Delete;

public class EventDeleteCommandHandler : IRequestHandler<EventDeleteCommand, ErrorOr<bool>>
{
    private readonly IRepository<Event> _eventRepository;

    public EventDeleteCommandHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<bool>> Handle(EventDeleteCommand request, CancellationToken cancellationToken)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
        if (existingEvent is null)
        {
            return  EventErrors.NotFoundError;
        }
        await _eventRepository.DeleteAsync(existingEvent);
        return true;
    }
}