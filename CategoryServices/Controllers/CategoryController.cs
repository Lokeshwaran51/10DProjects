using CategoryServices.Category.Queries;
using CategoryServices.Data.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryServices.Controllers
{
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
                List<CategoryDto> res = await _mediator.Send(new GetAllCategoriesQuery());
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }

        [HttpGet("GetSubCategoryByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                List<SubCategoryDto> res = await _mediator.Send(new GetSubCategoryByCategoryIdQuery(categoryId));
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }
    }
}
