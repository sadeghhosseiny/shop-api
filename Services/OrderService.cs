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

  public async Task<Order> AddOrderAsync(Order order)
  {
    await _context.Orders.AddAsync(order);
    await _context.SaveChangesAsync();
    return order;
  }

  public async Task<Order?> DeleteOrderAsync(int orderId)
  {
    var order = await _context.Orders
     .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null)
      return null;

    _context.Orders.Remove(order);

    await _context.SaveChangesAsync();

    return order;
  }
}