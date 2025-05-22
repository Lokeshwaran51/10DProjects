using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static AmazonClone.MVC.Models.ViewModel;

namespace AmazonClone.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress= new Uri(_configuration["ApiUrl:BaseUrl"]);
        }

        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> Products(int SubCategoryId)
        {
            var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/ProductControllerAPI/GetListOfProductsBySubCategoryId/{SubCategoryId}");
            List<Product> products = new List<Product>();
            if (res.IsSuccessStatusCode)
            {
                var data = await res.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return View(products);
        }

        [HttpGet("GetProductDetailByProductId/{Id}")]
        public async Task<IActionResult> ProductDetails(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/ProductControllerAPI/GetProductDetailByProductId/{Id}");
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound(); 
                }
                var productJson = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(productJson);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
