using System.ComponentModel.DataAnnotations.Schema;
using Assessment3.Server.Domain.UserEvents;

namespace Assessment3.Server.Domain.Common;

public class User
{
    public Guid Id { get; set; }
    public string Role { get; private set; }
    public string FirstName { get; private set; }
    [Column(TypeName = "VARBINARY(64)")]
    public byte[] Salt { get; set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    [Column(TypeName = "VARBINARY(64)")]
    public byte[] PasswordHash { get; private set; }
    public ICollection<UserEvent> UserEvents { get; set; }
    private User() {}

    public User(Guid id, string firstName, string lastName, string email, byte[] passwordHash,byte[] salt, string role)
    {
        Id = id;
        Role = role;
        FirstName = firstName;
        Salt = salt;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public static User Create(string firstName, string lastName, string email, byte[] passwordHash,byte[] salt, string role)
    {
        return new( Guid.NewGuid(), firstName, lastName, email, passwordHash, salt, role);
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (User)obj;
        return Email == other.Email;
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
    }
}