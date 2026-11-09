using ShopApi.Models;

namespace ShopApi.Services;

public interface IOrderService
{
  Task<IEnumerable<Order>?> GetUserOrders(int userId);
  Task<Order> AddOrderAsync(Order order);
  Task<Order?> DeleteOrderAsync(int orderId);
}