// Client/Auth/AuthService.cs
using Blazored.LocalStorage;
using ChamCongHaiChau.Client.Services;
using ChamCongHaiChau.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace ChamCongHaiChau.Client.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthStateProvider _authStateProvider;

    public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authProvider)
    {
        _http = http;
        _localStorage = localStorage;
        _authStateProvider = (JwtAuthStateProvider)authProvider;
    }

    public async Task<bool> Login(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/login", request);

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (result == null || string.IsNullOrWhiteSpace(result.Token))
            return false;

        await _localStorage.SetItemAsync("authToken", result.Token);

        // Lưu thông tin người dùng (nếu có từ response)
        if (result.currentUser != null)
        {
            await _localStorage.SetItemAsync("currentUser", result.currentUser);
        }

        _authStateProvider.NotifyUserAuthentication(result.Token);

        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

        return true;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("currentUser");
        _authStateProvider.NotifyUserLogout();
        _http.DefaultRequestHeaders.Authorization = null;
    }

    public bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var exp = jwtToken.ValidTo;

        return exp < DateTime.UtcNow;
    }

    public async Task<bool> CheckTokenAndLogoutIfExpired()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(token))
            return false;

        if (IsTokenExpired(token))
        {
            await Logout(); // 🔥 Tự logout luôn
            return true;
        }

        return false;
    }
}