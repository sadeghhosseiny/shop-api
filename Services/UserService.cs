using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Models;

namespace ShopApi.Services;

public class UserService : IUserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<User>> GetUsersAsync()
  {
    return await _context.Users.Include(u => u.Orders).ToListAsync();
  }

  public async Task<User> AddUserAsync(User user)
  {
    await _context.Users.AddAsync(user);

    await _context.SaveChangesAsync();

    return user;
  }

  public async Task<User?> GetUserByIdAsync(int id)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
  }
}