namespace ShopApi.DTOs;

public class RegisterDto
{
  public required string Name { get; set; }
  public required string PhoneNumber { get; set; }
  public required string Password { get; set; }
}