using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using ShopApi.Services;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly ICategoryService _categoryService;

  public CategoriesController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }

  // GET: api/categories
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Category>>> GetAll()
  {
    var categories = await _categoryService.GetAllAsync();
    return Ok(categories);
  }

  // GET: api/categories/id
  [HttpGet("{id}")]
  public async Task<ActionResult<Category>> GetOneCategory(int id)
  {
    var category = await _categoryService.GetCategoryById(id);

    if (category == null)
    {
      return NotFound(new
      {
        message = "There is no Category."
      });
    }

    return Ok(category);
  }

  // POST: api/categories
  [HttpPost]
  public async Task<ActionResult<Category>> Create([FromBody] Category category)
  {
    var createdCategory = await _categoryService.AddAsync(category);

    return CreatedAtAction(nameof(GetAll), new { id = createdCategory.Id }, createdCategory);
  }
}