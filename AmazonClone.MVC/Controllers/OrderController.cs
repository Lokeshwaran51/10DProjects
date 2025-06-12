using AmazonClone.MVC.Constant;
using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmazonClone.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IConfiguration configuration, ILogger<OrderController> logger)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> PlaceOrder(int ProductId, int quantity)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage productResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product/ProductDetails/{ProductId}");
                if (!productResponse.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Product not found.");
                    return NotFound("Product not found.");
                }
                string productJson = await productResponse.Content.ReadAsStringAsync();
                Product product = JsonConvert.DeserializeObject<Product>(productJson);
                if (product == null)
                {
                    return View("Error", "Failed to parse product data.");
                }
                PlaceOrderCommand placeOrderCommand = new PlaceOrderCommand
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    UserId = null,
                    Order = new OrderDetail
                    {
                        ProductName = product.Name,
                        Price = product.Price,
                        Total = Math.Round(product.Price * quantity, 2),
                    }
                };
                string jsonData = JsonConvert.SerializeObject(placeOrderCommand);
                StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage orderApiResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Order/PlaceOrder", content);
                if (!orderApiResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to place order. Please try again.");
                    _logger.LogWarning("Failed to place order.");
                    return View("Error");
                }
                Order confirmationModel = new Order
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Price = product.Price,
                    Total = Math.Round(product.Price * quantity, 2)
                };
                _logger.LogInformation("Order placed successfully.");
                return View("PlaceOrder", confirmationModel);
            }
            catch (Exception ex)
            {
                return View("Error", $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Success(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var wrapper = new
                    {
                        order = order
                    };
                    string token = HttpContext.Session.GetString("Token");
                    if (!string.IsNullOrEmpty(token))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }
                    string data = JsonConvert.SerializeObject(wrapper);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Order/Success", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Order placed with payment mode: {order.PaymentMode}";
                    }
                    else
                    {
                        _logger.LogError("Order failed.");
                        ModelState.AddModelError("", ResponseMessages.orderFailed);
                    }
                }
                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during the process request.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
