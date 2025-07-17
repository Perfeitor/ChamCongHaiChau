using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChamCongHaiChau.Shared.Models;

public class RegisterRequest
{
    [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
    public string Username { get; set; }

    [AllowNull]
    [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; }
}