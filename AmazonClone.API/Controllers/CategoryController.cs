using AmazonClone.API.Features.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var res = await _mediator.Send(new GetAllCategoriesQuery());
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetSubCategoryByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                var res = await _mediator.Send(new GetSubCategoryByCategoryIdQuery(categoryId));
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /*  [HttpGet("GetAllCategories")]
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
          }*/



        /* [HttpGet("GetSubCategoryByCategoryId/{categoryId}")]
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
         }*/
    }
}
