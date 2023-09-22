using System.ComponentModel.DataAnnotations;

namespace Assessment3.Shared.Models.Authentication;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "valid email address is required.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}