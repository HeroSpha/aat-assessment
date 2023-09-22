using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.Events.Errors;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, ErrorOr<EventResult>>
{
    private readonly IRepository<Event> _eventRepository;

    public UpdateCommandHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<EventResult>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
        if (existingEvent is null)
        {
            return  EventErrors.NotFoundError;
        }

        existingEvent.Title = request.Title;
        existingEvent.Description = request.Description;
        existingEvent.Image = request.Image;
        existingEvent.Date = request.Date;
        existingEvent.Venue = request.Venue;
        existingEvent.Seats = request.Seats;
         await _eventRepository.UpdateAsync(existingEvent);
         return new EventResult(existingEvent);
    }
}