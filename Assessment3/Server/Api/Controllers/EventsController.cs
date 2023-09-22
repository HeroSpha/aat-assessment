using Assessment3.Server.Application.Events.Commands.Create;
using Assessment3.Server.Application.Events.Commands.Delete;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Server.Application.Events.Queries.GetEvent;
using Assessment3.Server.Application.Events.Queries.GetEvents;
using Assessment3.Shared.Models.Events;
using MapsterMapper;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment3.Server.Controllers;
[Route("events")]
public class EventsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public EventsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(EventCreateRequest request)
    {
        var command = _mapper.Map<CreateEventCommand>(request);
        
        ErrorOr<EventResult> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            events => Ok(_mapper.Map<EventDto>(events)),
            Problem);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(EventCreateRequest request)
    {
        var command = _mapper.Map<CreateEventCommand>(request);
        
        ErrorOr<EventResult> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            events => Ok(_mapper.Map<EventDto>(events)),
            Problem);
    }
    [HttpGet("getlist")]
    public async Task<IActionResult> GetAllAsync()
    {
        var command = new GetEventsQuery();
        
        ErrorOr<EventsResult> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            events => Ok(_mapper.Map<IEnumerable<EventDto>>(events.Events)),
            Problem);
    }
    
    [HttpGet("getbyId/{id}")]
    public async Task<IActionResult> BetByIdAsync(Guid id)
    {
        var command = new GetEventQuery(id);
        
        ErrorOr<EventResult> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            events => Ok(_mapper.Map<EventDto>(events.Event)),
            Problem);
    }
    
    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new EventDeleteCommand(id);
        
        ErrorOr<bool> eventResult = await _mediator.Send(
            command);
        return eventResult.Match(
            events => Ok(events),
            Problem);
    }
}