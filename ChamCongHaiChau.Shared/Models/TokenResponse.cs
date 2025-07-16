namespace ChamCongHaiChau.Shared.Models;

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
    public ApplicationUser currentUser { get; set; } = new();
}