using Assessment3.Server.Application.Common.Services;

namespace Assessment3.Server.Infrastructure.Common.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}