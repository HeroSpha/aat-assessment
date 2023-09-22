using Newtonsoft.Json;

namespace Assessment3.Client.Models;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
    [JsonExtensionData]
    public Dictionary<string, object> Errors { get; set; }
}