using System.ComponentModel.DataAnnotations;

namespace Assessment3.Shared.Models.Authentication;

public class RegisterRequest
{
    [Required(ErrorMessage = "First name is required")]
    public  string FirstName { get; set; }
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "valid email address is required.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}