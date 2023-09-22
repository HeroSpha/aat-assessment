using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Assessment3.Server.Domain.Common;
using Assessment3.Server.Domain.Events;

namespace Assessment3.Server.Domain.UserEvents;

public class UserEvent
{
    public Guid Id { get; set; }
    
    public Guid EventId { get; set; }
    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    public DateTime Date { get; set; }
    public int Reference { get; set; }

    private UserEvent(Guid id, Guid userId, Guid eventId)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        Date = DateTime.Now;
        Reference = ConvertToPositiveInt(id);
    }
    
    private static int ConvertToPositiveInt(Guid guid)
    {
        
        var bytes = guid.ToByteArray();
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(bytes);
        var result = BitConverter.ToInt32(hashBytes, 0);
        
        if (result < 0)
        {
            result = -result;
        }

        return result;
    }

    public static UserEvent Create(Guid userId, Guid eventId)
    {
        return new(Guid.NewGuid(), userId, eventId);
    }
}