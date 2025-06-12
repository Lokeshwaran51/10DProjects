using AmazonClone.MVC.Constant;
using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmazonClone.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IConfiguration configuration, ILogger<ProductController> logger)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
            _logger = logger;
        }

        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> Products(int SubCategoryId)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product/GetListOfProductsBySubCategoryId/{SubCategoryId}");
                List<Product> products = new List<Models.Product>();
                if (res.IsSuccessStatusCode)
                {
                    string data = await res.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(data);
                }
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during the process request.");
                return StatusCode(500, ResponseMessages.internalServerError + ex.Message);
            }
        }


        [HttpGet("ProductDetails/{Id}")]
        public async Task<IActionResult> ProductDetails(int Id)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product/ProductDetails/{Id}");
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                string productJson = await response.Content.ReadAsStringAsync();
                Product product = JsonConvert.DeserializeObject<Product>(productJson);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during the process request.");
                return StatusCode(500, ResponseMessages.internalServerError + ex.Message);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
