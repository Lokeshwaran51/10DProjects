using AmazonClone.API.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoryController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories 
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
            try
            {
                var subcategories = await _context.SubCategories
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
