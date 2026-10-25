using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Models;

namespace ShopApi.Services;

public class CategoryService : ICategoryService
{
  private readonly AppDbContext _context;

  public CategoryService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Category>> GetAllAsync()
  {
    return await _context.Categories.ToListAsync();
  }

  public async Task<Category?> GetCategoryById(int id)
  {
    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

    return category;
  }

  public async Task<Category> AddAsync(Category category)
  {
    await _context.Categories.AddAsync(category);

    await _context.SaveChangesAsync();

    return category;
  }
}