using Assessment3.Server.Domain.UserEvents;

namespace Assessment3.Server.Domain.Events
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public int Seats { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
        private Event(Guid id, string title, string description, string image, DateTime date, string venue, int seats)
        {
            Id = id;
            Title = title;
            Description = description;
            Image = image;
            Date = date;
            Venue = venue;
            Seats = seats;
        }

        public static Event Create(string title, string description, string image, DateTime date, string venue, int seats)
        {
            //enforce invariants 
            return new(Guid.NewGuid(), title, description, image, date, venue, seats);
        }
    }
}
