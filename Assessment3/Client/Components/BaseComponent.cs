using Assessment3.Client.Models;
using Blazored.Toast.Services;
using Flurl.Http;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text;

namespace Assessment3.Client.Components;

public class BaseComponent : ComponentBase
{
    protected StringBuilder ErrorMessage = new StringBuilder();
    protected bool shouldRender;
    [Inject] public required IToastService ToastService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected async Task GetErrors(FlurlHttpException ex)
    {
        if (ex.Call?.Response != null)
        {
            var error = await ex.GetResponseJsonAsync<ErrorResponse>();
            if (error != null)
            {
                ErrorMessage.AppendLine(error.Title);
                if (error.Errors is {Count: > 0})
                {
                    foreach (var er in error.Errors)
                    {
                        ErrorMessage.AppendLine($"{er.Key}: {string.Join(", ", er.Value as IEnumerable<string>)}");
                    }
                }

            }
        }
    }
}