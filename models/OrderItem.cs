namespace ShopApi.Models;

public class OrderItem
{
  public int Id { get; set; }

  public int OrderId { get; set; }
  public Order Order { get; set; } = null!;

  public int ProductId { get; set; }
  public Product Product { get; set; } = null!;

  public int Quantity { get; set; } // تعداد خریداری شده از این محصول
  public decimal UnitPrice { get; set; } // قیمت محصول در زمان خرید
}