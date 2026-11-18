using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Data;
using ShopApi.DTOs;
using ShopApi.Models;

namespace ShopApi.Services;

public class AuthService : IAuthService
{
  private readonly AppDbContext _context;
  private readonly IConfiguration _configuration;
  private readonly IPasswordHasher<User> _passwordHasher;

  public AuthService(AppDbContext context, IConfiguration configuration)
  {
    _context = context;
    _configuration = configuration;
    _passwordHasher = new PasswordHasher<User>();
  }

  public async Task<string> RegisterAsync(RegisterDto dto)
  {
    // ۱. بررسی تکراری نبودن شماره موبایل یا ایمیل
    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
    if (existingUser != null)
    {
      throw new Exception("کاربری با این شماره قبلاً ثبت‌ نام کرده است.");
    }

    // ۲. ساخت کاربر جدید
    var user = new User
    {
      Name = dto.Name,
      PhoneNumber = dto.PhoneNumber,
      Role = "User"
    };

    // ۳. هش کردن پسورد
    user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

    // ۴. ذخیره در دیتابیس
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();

    // ۵. تولید و بازگرداندن توکن JWT
    return GenerateJwtToken(user);
  }

  public async Task<string?> LoginAsync(LoginDto dto)
  {
    // ۱. پیدا کردن کاربر بر اساس ایمیل (یا شماره موبایل)
    var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

    // اگر کاربر پیدا نشد (طبق خواسته شما که باید برود سمت رجیستر)
    if (user == null)
    {
      return null;
    }

    // ۲. بررسی صحت پسورد
    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
    if (result == PasswordVerificationResult.Failed)
    {
      throw new Exception("رمز عبور اشتباه است.");
    }

    // ۳. تولید و بازگرداندن توکن JWT
    return GenerateJwtToken(user);
  }

  // متد کمکی برای ساخت توکن JWT
  private string GenerateJwtToken(User user)
  {
    var jwtSettings = _configuration.GetSection("Jwt");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Role)
        };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(7), // اعتبار ۷ روزه توکن
      Issuer = jwtSettings["Issuer"],
      Audience = jwtSettings["Audience"],
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}