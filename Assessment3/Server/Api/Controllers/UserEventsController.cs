using Assessment3.Server.Application.Events.Commands.Create;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.UserEvents.Commands.Create;
using Assessment3.Server.Application.UserEvents.Commons;
using Assessment3.Shared.Models.Events;
using Assessment3.Shared.Models.UserEvents;
using MapsterMapper;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Assessment3.Server.Controllers;
[Route("user-events")]
public class UserEventsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserEventsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Create(CreateUserEventRequest request)
    {
        var command = _mapper.Map<CreateUserEventCommand>(request);
        
        ErrorOr<UserEventResult> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            userEvent => Ok(_mapper.Map<UserEventDto>(userEvent)),
            Problem);
    }

}