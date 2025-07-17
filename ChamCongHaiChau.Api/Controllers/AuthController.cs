using ChamCongHaiChau.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = ChamCongHaiChau.Shared.Models.LoginRequest;
using RegisterRequest = ChamCongHaiChau.Shared.Models.RegisterRequest;

namespace ChamCongHaiChau.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IAuthService authService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        try
        {
            var result = await _authService.Register(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new LoginResponse { IsSuccess = false, Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        try
        {
            var response = (LoginResponse)(await _authService.Login(model));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new RegisterResponse { IsSuccess = false, Message = ex.Message });
        }
    }
}
