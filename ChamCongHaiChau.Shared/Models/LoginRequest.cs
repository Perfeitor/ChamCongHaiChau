using System.ComponentModel.DataAnnotations;

namespace ChamCongHaiChau.Shared.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Tên tài khoản không được để trống")]
    public string Email { get; set; } = default!;
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; } = true;
}
