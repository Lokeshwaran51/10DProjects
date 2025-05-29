using AmazonClone.API.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _context = appDbContext;
        }

        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> GetListOfProductsBySubCategoryId(int SubCategoryId)
        {
            try
            {
                var products = await _context.Products
                       .Where(p => p.SubCategoryId == SubCategoryId)
                       .ToListAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("ProductDetails/{ProductId}")]
        public async Task<IActionResult> ProductDetails(int ProductId)
        {
            try
            {
                if (ProductId <= 0)
                {
                    return BadRequest("Invalid product ID");
                }; 
                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync(p => p.Id == ProductId);

                if (product == null)
                {
                    return NotFound($"Product with ID {ProductId} not found");
                }
                var result = new
                {
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.ImageUrl,
                    CategoryName = product.Category?.Name,
                    SubCategoryName = product.SubCategory?.SubCategoryName ?? product.SubCategoryName
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
