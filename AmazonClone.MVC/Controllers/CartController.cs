using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace AmazonClone.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int ProductId, int Quantity)
        {
            try
            {
                var data = JsonConvert.SerializeObject(new { productId = ProductId, quantity = Quantity });
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Cart/AddToCart", content);

                return RedirectToAction("ProductDetails", "Product", new { id = ProductId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
