using ChamCongHaiChau.Api.Data;
using ChamCongHaiChau.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChamCongHaiChau.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ChamCongHaiChauDbContext _db;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, ChamCongHaiChauDbContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _db = db;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(ApplicationUser user, bool rememberMe)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? _configuration["Jwt:Default"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                claims: new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
                },
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Username);
                if (user == null)
                    user = await _userManager.FindByNameAsync(request.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    LoginResponse response = new();
                    response.Token = GenerateJwtToken(user, request.RememberMe);
                    response.currentUser = user;
                    response.IsSuccess = true;
                    return response;
                }

                return new();
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var existingUser = await _userManager.FindByNameAsync(model.Username);
                if (existingUser != null)
                {
                    return new RegisterResponse
                    {
                        IsSuccess = false,
                        Message = "Tên đăng nhập đã tồn tại."
                    };
                }
                else
                {
                    existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null)
                    {
                        return new RegisterResponse
                        {
                            IsSuccess = false,
                            Message = "Email đã được sử dụng."
                        };
                    }
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return new RegisterResponse
                    {
                        IsSuccess = true,
                    };

                return new RegisterResponse
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public Task<bool> CheckTokenAndLogoutIfExpired()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}