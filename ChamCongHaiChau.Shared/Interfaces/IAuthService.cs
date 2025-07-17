using ChamCongHaiChau.Shared.Models;

public interface IAuthService
{
    Task<bool> CheckTokenAndLogoutIfExpired();
    Task<bool> Login(LoginRequest request);
    Task Logout();
}
