using ShopApi.DTOs;
using ShopApi.Models;

namespace ShopApi.Services;

public interface IAuthService
{
  Task<string> RegisterAsync(RegisterDto dto);
  Task<string?> LoginAsync(LoginDto dto);
}