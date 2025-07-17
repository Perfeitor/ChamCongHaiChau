using ChamCongHaiChau.Shared.Models;

public interface IAuthService
{
    Task<bool> CheckTokenAndLogoutIfExpired();
    Task<ApplicationUser> GetCurrentUser();
    Task<LoginResponse> Login(LoginRequest request);
    Task Logout();
    Task<RegisterResponse> Register(RegisterRequest model);
}
