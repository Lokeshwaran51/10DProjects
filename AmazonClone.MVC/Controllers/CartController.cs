using AmazonClone.MVC.Models;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace AmazonClone.MVC.Controllers
{
    [Route("Cart")]
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


        [HttpGet("ViewCart")]
        public async Task<IActionResult> ViewCart([FromQuery] string Email)
        {
            try
            {
                var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Cart/GetCartItems/{Email}");
                if (res.IsSuccessStatusCode)
                {
                    var cartItems = await res.Content.ReadFromJsonAsync<List<CartItemDto>>();
                    return View(cartItems);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, int quantity, string UserId)
        {
            try
            {
                var email = HttpContext.Session.GetString("Email");
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User not logged in.");
                }
                var formData = new Dictionary<string, string>
                {
                    { "Email", email },
                    { "productId", productId.ToString() },
                    { "quantity", quantity.ToString() }
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync("/api/Cart/AddToCart", content);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"API error: {error}");
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Added to cart!" });
                }
                return RedirectToAction("Products", "Product", new { id = productId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int ProductId)
        {
            try
            {
                var data = JsonConvert.SerializeObject(ProductId);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/Cart/RemoveFromCart?ProductId={ProductId}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product Removed from Cart.";
                    return RedirectToAction("ViewCart", "Cart");
                }
                return RedirectToAction("ViewCart", "Cart");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
