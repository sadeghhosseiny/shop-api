using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using ShopApi.Services;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  // GET: api/users
  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await _userService.GetUsersAsync();
    return Ok(users);
  }

  // POST: api/user
  [HttpPost("/api/user")]
  public async Task<ActionResult<User>> addUser(User user)
  {
    await _userService.AddUserAsync(user);

    return Ok(user);
  }

  // GET: api/user/{id}
  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUserById(int id)
  {
    var user = await _userService.GetUserByIdAsync(id);
    if (user == null)
    {
      return NotFound();
    }
    
    return Ok(user);
  }
}