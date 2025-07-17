namespace ChamCongHaiChau.Shared.Models;

public class LoginResponse
{
    public bool IsSuccess { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public ApplicationUser currentUser { get; set; } = new();
    public string? Message { get; set; }
}