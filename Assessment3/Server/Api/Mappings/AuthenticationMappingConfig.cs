using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Authentication.Queries.Login;
using Assessment3.Shared.Models.Authentication;
using Mapster;

namespace Assessment3.Server.Api.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.FirstName, scr => scr.User.FirstName)
            .Map(dest => dest.LastName, scr => scr.User.LastName)
            .Map(dest => dest.Email, scr => scr.User.Email)
            .Map(dest => dest.Role, scr => scr.User.Role)
            .Map(dest => dest.Id, scr => scr.User.Id);
        config.NewConfig<LoginRequest, LoginQuery>();
        // config.NewConfig<User, GetUserResponse>()
        //     .Map(dest => dest.FirstName, scr => scr.FirstName)
        //     .Map(dest => dest.LastName, scr => scr.LastName)
        //     .Map(dest => dest.Email, scr => scr.Email)
        //     .Map(dest => dest.Role, scr => scr.Role)
        //     .Map(dest => dest.Id, scr => scr.Id.Value);
    }
}