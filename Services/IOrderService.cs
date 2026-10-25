using ShopApi.Models;

namespace ShopApi.Services;

public interface IOrderService
{
  Task<IEnumerable<Order>> GetUserOrders(int userId);
}