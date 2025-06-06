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

        public OrderController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
        }

        /* [HttpGet]
         public async Task<IActionResult> PlaceOrder()
         {
             return View();
         }*/

        /* [HttpGet]
         public async Task<IActionResult> PlaceOrder(int ProductId, int quantity)
         {
             try
             {
                 var productResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product/ProductDetails/{ProductId}");
                 if (!productResponse.IsSuccessStatusCode)
                 {
                     return NotFound("Product not found.");
                 }

                 var productJson = await productResponse.Content.ReadAsStringAsync();
                 var product = JsonConvert.DeserializeObject<Product>(productJson);
                 if (product == null)
                 {
                     return View("Error", "Failed to parse product data.");
                 }

                 // 2. Prepare order data
                 var orderData = new Order
                 {
                     Id = product.Id,
                     ProductName = product.Name,
                     Quantity = quantity,
                     Price = product.Price,
                     Total = Math.Round(product.Price * quantity, 2)
                 };

                 var jsonData = JsonConvert.SerializeObject(orderData);
                 var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                 // 3. Send order request to Order API
                 var orderApiResponse = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Order/PlaceOrder", content);

                 if (!orderApiResponse.IsSuccessStatusCode)
                 {
                     ModelState.AddModelError("", "Failed to place order. Please try again.");
                     return View("Error");
                 }

                 // 4. Return confirmation view
                 var confirmationModel = new Order
                 {
                     Id = product.Id,
                     ProductName = product.Name,
                     PaymentMode = orderData.PaymentMode,
                     Quantity = quantity,
                     Price = product.Price,
                     Total = orderData.Total
                 };

                 return View("PlaceOrder", confirmationModel);
             }
             catch (Exception ex)
             {
                 return View("Error", $"Unexpected error: {ex.Message}");
             }
         }*/

        [HttpGet]
        public async Task<IActionResult> PlaceOrder(int ProductId, int quantity)
        {
            try
            {
                HttpResponseMessage productResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product/ProductDetails/{ProductId}");
                if (!productResponse.IsSuccessStatusCode)
                {
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
                HttpResponseMessage orderApiResponse = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Order/PlaceOrder", content);

                if (!orderApiResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to place order. Please try again.");
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

                    string data = JsonConvert.SerializeObject(wrapper);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Order/Success", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Order placed with payment mode: {order.PaymentMode}";
                    }
                    else
                    {
                        ModelState.AddModelError("",ResponseMessages.orderFailed);
                    }
                }
                return View(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        /*[HttpPost]
        public async Task<IActionResult> Success()
        {
            var data = JsonConvert.SerializeObject();
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Order/Success", content);
            return View();
        }*/


        public IActionResult Index()
        {
            return View();
        }
    }
}
