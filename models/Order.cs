using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShopApi.Models;

public class Order
{
  public int Id { get; set; }
  public DateTime OrderDate { get; set; } = DateTime.UtcNow;
  public string Status { get; set; } = "Pending"; // Pending (در انتظار پرداخت), Paid (پرداخت شده)
  public decimal TotalPrice { get; set; }

  public int UserId { get; set; }

  [JsonIgnore]
  [ValidateNever]
  public User User { get; set; } = null!;

  public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}