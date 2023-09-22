using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.UserEvents.Commons;
using Assessment3.Server.Domain.Common;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.UserEvents;
using Assessment3.Server.Domain.UserEvents.Errors;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.UserEvents.Commands.Create;

public class CreateUserEventCommandHandler : IRequestHandler<CreateUserEventCommand, ErrorOr<UserEventResult>>
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<UserEvent> _userEventRepository;
    private readonly IRepository<User> _userRepository;

    public CreateUserEventCommandHandler(IRepository<Event> eventRepository, IRepository<UserEvent> userEventRepository, IRepository<User> userRepository)
    {
        _eventRepository = eventRepository;
        _userEventRepository = userEventRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserEventResult>> Handle(CreateUserEventCommand request, CancellationToken cancellationToken)
    {
        var userEvent = await _userEventRepository.FirstOrDefaultAsync(
            x => x.EventId.Equals(request.EventId) && x.UserId.Equals(request.UserId));
        if (userEvent is not null)
        {
            return UserEventErrors.RegisteredError;
        }
        userEvent = UserEvent.Create(request.UserId, request.EventId);
        await _userEventRepository.InsertAsync(userEvent);
        return new UserEventResult(userEvent);
    }
}