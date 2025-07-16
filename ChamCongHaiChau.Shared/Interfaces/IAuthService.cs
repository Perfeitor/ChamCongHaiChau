using ChamCongHaiChau.Shared.Models;

public interface IAuthServices
{
    Task<bool> CheckTokenAndLogoutIfExpired();
    Task<bool> Login(LoginRequest request);
    Task Logout();
}
