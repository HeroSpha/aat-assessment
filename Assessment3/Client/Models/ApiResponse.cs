namespace Assessment3.Client.Models;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public ErrorResponse? ErrorResponse { get; set; }
    public bool IsSuccess { get; set; }
}