using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Models;

namespace ShopApi.Services;

public class OrderService : IOrderService
{
  private readonly AppDbContext _context;

  public OrderService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Order>?> GetUserOrders(int userId)
  {
    var userExists = await _context.Users
        .AnyAsync(u => u.Id == userId);

    if (!userExists)
    {
      return null;
    }

    var orders = await _context.Orders
        .Where(o => o.UserId == userId)
        .ToListAsync();

    return orders;
  }
}