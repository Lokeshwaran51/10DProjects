using AmazonClone.API.CQRS.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubCategoryId"></param>
        /// <returns></returns>
        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> GetListOfProductsBySubCategoryId(int SubCategoryId)
        {
            try
            {
                var res = await _mediator.Send(new GetListOfProductsBySubCategoryIdQuery(SubCategoryId));
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ProductDetails/{ProductId}")]
        public async Task<IActionResult> ProductDetails(int ProductId)
        {
            try
            {
                var res = await _mediator.Send(new ProductDetailsQuery(ProductId));
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
