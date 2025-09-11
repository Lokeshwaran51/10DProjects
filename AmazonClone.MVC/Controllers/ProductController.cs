using AmazonClone.MVC.Constant;
using AmazonClone.MVC.Models;
using AmazonClone.MVC.Services;
using AmazonClone.MVC.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmazonClone.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;
        public ProductController(ProductService productService,ILogger<ProductController> logger)
        {
            /*_configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);*/
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
        public async Task<IActionResult> Products(int SubCategoryId)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                string email = HttpContext.Session.GetString("Email");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                {
                    return Unauthorized(ResponseMessages.userNotLoggedIn);
                }

                var response = await _productService.GetListOfProductsBySubCategoryId(SubCategoryId, token);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"API Error: {error}");
                }
                return View(response);
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
                string email = HttpContext.Session.GetString("Email");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                {
                    return Unauthorized(ResponseMessages.userNotLoggedIn);
                }

                var response = await _productService.ProductDetails(Id, token);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"API Error: {error}");
                }
                string productJson = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(productJson);
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

        /* [HttpGet("GetListOfProductsBySubCategoryId/{SubCategoryId}")]
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
         }*/


        /* [HttpGet("ProductDetails/{Id}")]
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
         }*/
        public IActionResult Index()
        {
            return View();
        }
    }
}
