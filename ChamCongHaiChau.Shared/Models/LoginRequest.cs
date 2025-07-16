using System.ComponentModel.DataAnnotations;

namespace ChamCongHaiChau.Shared.Models;

public class LoginRequest
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; } = true;
}
