using ShopApi.Models;

namespace ShopApi.Services;

public interface ICategoryService
{
  Task<IEnumerable<Category>> GetAllAsync();
  Task<Category?> GetCategoryById(int id);
  Task<Category> AddAsync(Category category);
}