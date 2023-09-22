using System.ComponentModel.DataAnnotations;

namespace Assessment3.Shared.Models.Events;

public class UpdateEventRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Image is required.")]
    public string Image { get; set; }
    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }
    [Required(ErrorMessage = "Venue is required.")]
    public string Venue { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Seats must be greater than 0.")]
    public int Seats { get; set; }
}