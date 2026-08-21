using System.ComponentModel.DataAnnotations;

namespace TaskManager.Blazor.Models;

public class RegisterRequest
{
    [Required(
        ErrorMessage = "Name is required.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage =
            "Name must be between 3 and 100 characters.")]
    public string Name { get; set; } =
        "";


    [Required(
        ErrorMessage = "Email is required.")]
    [EmailAddress(
        ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } =
        "";


    [Required(
        ErrorMessage = "Password is required.")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage =
            "Password must be at least 6 characters.")]
    public string Password { get; set; } =
        "";
}