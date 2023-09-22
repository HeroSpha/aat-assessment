using Assessment3.Server.Application.UserEvents.Commands.Create;
using Assessment3.Server.Application.UserEvents.Commons;
using Assessment3.Shared.Models.UserEvents;
using Mapster;

namespace Assessment3.Server.Api.Mappings;

public class UserEventMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserEventRequest, CreateUserEventCommand>();
        config.NewConfig<UserEventResult, UserEventDto>();
    }
}