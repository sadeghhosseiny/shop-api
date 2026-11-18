using Microsoft.AspNetCore.Mvc;
using ShopApi.DTOs;
using ShopApi.Services;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterDto dto)
  {
    try
    {
      var token = await _authService.RegisterAsync(dto);
      return Ok(new { token, message = "ثبت‌نام با موفقیت انجام شد." });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDto dto)
  {
    try
    {
      var token = await _authService.LoginAsync(dto);

      if (token == null)
      {
        return NotFound(new { message = "کاربری با این مشخصات یافت نشد. لطفاً ثبت‌نام کنید." });
      }

      return Ok(new { token, message = "ورود موفقیت‌آمیز بود." });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }
}