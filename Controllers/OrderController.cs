using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using ShopApi.Services;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
  private readonly IOrderService _orderService;

  public OrderController(IOrderService orderService)
  {
    _orderService = orderService;
  }

  // GET: api/order/id
  [HttpGet("{userId}")]
  public async Task<ActionResult<IEnumerable<Order>>> GetOrder(int userId)
  {
    var orders = await _orderService.GetUserOrders(userId);

    if (orders == null)
    {
      return NotFound(new
      {
        message = "User not found."
      });
    }

    return Ok(orders);
  }

  // POST: api/order
  [HttpPost]
  public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
  {
    var newOrder = await _orderService.AddOrderAsync(order);
    return CreatedAtAction(nameof(GetOrder), new { userId = newOrder.UserId }, newOrder);
  }

  // GET: api/order/id
  [HttpDelete("{orderId}")]
  public async Task<ActionResult<Order?>> DeleteOrder(int orderId)
  {
    var order = await _orderService.DeleteOrderAsync(orderId);

    if (order == null)
      return NotFound(new { message = "Order not found." });

    return Ok(order);
  }

  // api/order/{orderId}/orderItem
  [HttpPost("{orderId}/orderItem")]
    public async Task<ActionResult<OrderItem>> AddOrderItem(int orderId, [FromBody] OrderItem item)
    {
        try
        {
          var createdItem = await _orderService.AddOrderItemToOrderAsync(orderId, item);
          return Ok(createdItem); 
        }
        catch (KeyNotFoundException)
        {
          return NotFound($"Order with ID {orderId} not found.");
        }
    }
}