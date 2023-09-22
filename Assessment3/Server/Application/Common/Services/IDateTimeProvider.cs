namespace Assessment3.Server.Application.Common.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}