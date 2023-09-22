namespace Assessment3.Shared.Models.Events;

public record EventDto(
    Guid Id, 
    string Title, 
    string Description, 
    string Image, 
    DateTime Date, 
    string Venue, 
    int Seats);