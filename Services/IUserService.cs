using ShopApi.Models;

namespace ShopApi.Services;

public interface IUserService
{
  Task<IEnumerable<User>> GetUsersAsync();
  Task<User?> GetUserByIdAsync(int id);
  Task<User> AddUserAsync(User user);
}