using Assessment3.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Assessment3.Client.Services;

public class BaseService
{
    protected readonly ITokenService TokenService;

    protected BaseService(ITokenService tokenService)
    {
        TokenService = tokenService;
    }
}