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
  [HttpGet("{id}")]
  public async Task<ActionResult<IEnumerable<Order>>> GetOrder(int userId)
  {
    var orders = await _orderService.GetUserOrders(userId);
    return Ok(orders);
  }
}