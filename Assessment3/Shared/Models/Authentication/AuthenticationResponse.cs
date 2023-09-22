namespace Assessment3.Shared.Models.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Role,
    string Email,
    string Token);