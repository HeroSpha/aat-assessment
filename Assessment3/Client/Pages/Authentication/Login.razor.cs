using Assessment3.Client.Components;
using Assessment3.Client.Configuration;
using Assessment3.Client.Models;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Authentication;
using Blazored.LocalStorage;
using Flurl.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Text.Json;

namespace Assessment3.Client.Pages.Authentication;

public partial class Login : BaseComponent
{
    private LoginRequest LoginModel = new();
    private RegisterRequest RegisterModel = new();
    [Inject] public required IAuthenticationService AuthenticationService { get; set; }
    [Inject] public required ILocalStorageService LocalStorageService { get; set; }
    [Inject] public AuthenticationStateProvider State { get; set; }

    private bool IsRegistering = false;
    private void Register()
    {
        ErrorMessage.Clear();
        IsRegistering = true;
    }
    private void SignIn()
    {
        ErrorMessage.Clear();
        IsRegistering = false;
    }
    private async Task HandleSubmit()
    {
        try
        {
            ErrorMessage.Clear();
            shouldRender = false;
            var response = await AuthenticationService.Register(RegisterModel);
            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var user = await response.GetJsonAsync<AuthenticationResponse>();
                await LocalStorageService.SetItemAsync(UserConfig.User, user);
                await State.GetAuthenticationStateAsync();
            }

        }
        catch (FlurlHttpException ex)
        {
            await GetErrors(ex);
        }
    }
    private async Task Submit()
    {
        try
        {
            ErrorMessage.Clear();
           
            shouldRender = false;
            var response = await AuthenticationService.Login(LoginModel.Email, LoginModel.Password);
            if (response.StatusCode == 200)
            {
                var user = await response.GetJsonAsync<AuthenticationResponse>();
                await LocalStorageService.SetItemAsync(UserConfig.User, user);
                await State.GetAuthenticationStateAsync();
            }
            
            else
            {
                //display error messages
                ErrorMessage.AppendLine("Invalid username or password");
            }
        }
        catch (FlurlHttpException ex)
        {
            await GetErrors(ex);
        }
        finally
        {
            shouldRender = true;
            StateHasChanged();
        }
    }
    

    protected override bool ShouldRender()
    {
        return shouldRender;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        shouldRender = true;
    }
}