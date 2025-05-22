using AmazonClone.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoryControllerAPI(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories
                    //.Include(c => c.SubCategory)  
                    .ToListAsync();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("GetSubCategoryByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubCategoryByCategoryId(int categoryId)
        {
            var subcategories = await _context.SubCategory
             .Include(sc => sc.Category) 
             .Where(sc => sc.CategoryId == categoryId)
             .Select(sc => new
             {
                 sc.SubCategoryId,
                 sc.SubCategoryName,
                 sc.CategoryId,
                 CategoryName = sc.Category.Name 
             })
             .ToListAsync();
            return Ok(subcategories);
        }
    }
}
