using Assessment3.Client.Models;
using Flurl;
using Flurl.Http;

namespace Assessment3.Client.Helpers;

public static class FlurlHelper
{
    public static async Task<ApiResponse<T>> SendRequestAsync<T>(
        Func<IFlurlRequest, Task<IFlurlResponse>> requestFunc, 
        string endPoint, object payload)
    {
        var apiResponse = new ApiResponse<T>();

        try
        {
            var response = await requestFunc(
                Constants.AppUrl
                    .AppendPathSegment(endPoint)
                    .WithTimeout(TimeSpan.FromSeconds(30))
                     );

            if (response.StatusCode == 200)
            {
                apiResponse.Data = await response.GetJsonAsync<T>();
                apiResponse.IsSuccess = true;
            }
            else
            {
                var ar  = await response.GetJsonAsync<ErrorResponse>();
                apiResponse.IsSuccess = false;
            }
        }
        catch (FlurlHttpException ex)
        {
            //apiResponse.ErrorMessage = $"HTTP Request Error: {ex.Message}";
            apiResponse.IsSuccess = false;
        }

        return apiResponse;
    }
}