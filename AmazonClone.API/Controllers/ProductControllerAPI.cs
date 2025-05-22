using AmazonClone.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public ProductControllerAPI(IConfiguration configuration,AppDbContext appDbContext)
        {
            _configuration = configuration;
            _context = appDbContext;
        }

        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> GetListOfProductsBySubCategoryId(int SubCategoryId)
        {
            var products = await _context.Products
                .Where(p => p.SubCategoryId == SubCategoryId)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("GetProductDetailByProductId/{id}")]
        public async Task<IActionResult> GetProductDetailByProductId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid product ID");
                }; ;
                var product = await _context.Products
                    .Include(p => p.Category)  
                    .Include(p => p.SubCategory)  
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
