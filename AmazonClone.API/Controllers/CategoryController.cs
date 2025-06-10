using AmazonClone.API.CQRS.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    [Authorize]
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
    }
}
