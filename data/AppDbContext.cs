using Microsoft.EntityFrameworkCore;
using ShopApi.Models;

namespace ShopApi.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  // معرفی جدول‌ها به دیتابیس
  public DbSet<User> Users => Set<User>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // تنظیمات پیشرفته روابط (در صورت نیاز)
    // مثلاً تعیین دقت قیمت‌ها برای اعشار
    modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");

    modelBuilder.Entity<Order>()
        .Property(o => o.TotalPrice)
        .HasColumnType("decimal(18,2)");

    modelBuilder.Entity<OrderItem>()
        .Property(oi => oi.UnitPrice)
        .HasColumnType("decimal(18,2)");
  }
}