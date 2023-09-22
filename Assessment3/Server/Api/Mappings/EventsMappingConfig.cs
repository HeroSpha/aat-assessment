using Assessment3.Server.Application.Events.Commands.Create;
using Assessment3.Server.Application.Events.Commom;
using Assessment3.Shared.Models.Events;
using Mapster;

namespace Assessment3.Server.Api.Mappings;

public class EventsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EventCreateRequest, CreateEventCommand>();
        config.NewConfig<EventResult, EventDto>()
            .Map(dest => dest.Title, scr => scr.Event.Title)
            .Map(dest => dest.Description, scr => scr.Event.Description)
            .Map(dest => dest.Venue, scr => scr.Event.Venue)
            .Map(dest => dest.Seats, scr => scr.Event.Seats)
            .Map(dest => dest.Image, scr => scr.Event.Image)
            .Map(dest => dest.Id, scr => scr.Event.Id);
    }
}