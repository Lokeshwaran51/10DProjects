using AmazonClone.MVC.Constant;
using AmazonClone.MVC.Models;
using AmazonClone.MVC.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

namespace AmazonClone.MVC.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        /*  private readonly HttpClient _httpClient;
          private readonly IConfiguration _configuration;*/
        private readonly ILogger<CartController> _logger;
        private readonly CartService _cartService;

        public CartController(CartService cartService, ILogger<CartController> logger)
        {
            /* _httpClient = new HttpClient();
             _configuration = configuration;
             _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
             _logger = logger;*/
            _cartService = cartService;
            _logger = logger;
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartDTO dto)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                string email = HttpContext.Session.GetString("Email");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                {
                    return Unauthorized(ResponseMessages.userNotLoggedIn);
                }

                dto.Email = email;

                var response = await _cartService.AddToCart(dto, token);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"API Error: {error}");
                }

                // If you want to redirect or show success
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Product added to cart successfully!" });
                }

                return RedirectToAction("Products", "Product", new { id = dto.ProductId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ViewCart")]
        public async Task<IActionResult> ViewCart(string Email)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                var res = await _cartService.ViewCart(Email, token);
                if (!res.IsSuccessStatusCode)
                {
                    List<CartItemDTO> cartItems = await res.Content.ReadFromJsonAsync<List<CartItemDTO>>();
                    return View(cartItems);
                }
                return View(null);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("RemoveItemFromCart")]
        public async Task<IActionResult> RemoveItemFromCart(int ProductId)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                var res = await _cartService.RemoveItemFromCart(ProductId, token);
                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = ResponseMessages.productRemoved;
                    _logger.LogInformation("Product removed from cart successfully.");
                    return RedirectToAction("ViewCart", "Cart");
                }
                return View();
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

        /*[HttpGet("ViewCart")]
        public async Task<IActionResult> ViewCart([FromQuery] string Email)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Cart/GetCartItems/{Email}");
                if (res.IsSuccessStatusCode)
                {
                    List<CartItemDTO> cartItems = await res.Content.ReadFromJsonAsync<List<CartItemDTO>>();
                    return View(cartItems);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during the process request.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromForm]AddToCartDTO dto)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                string email = HttpContext.Session.GetString("Email");
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(ResponseMessages.userNotLoggedIn);
                }

                AddToCartDTO addToCartDTO = new AddToCartDTO
                {
                    Email = email,
                    ProductId = productId,
                    Quantity = quantity,
                };

                string data = JsonConvert.SerializeObject(addToCartDTO);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _cartService.AddToCart(dto,token);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"API error: {error}");
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Added to cart!" });
                }
                _logger.LogInformation("Product added to cart successfully.");
                return RedirectToAction("Products", "Product", new { id = productId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during the process request.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("RemoveItemFromCart")]
        public async Task<IActionResult> RemoveItemFromCart(int ProductId)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                string data = JsonConvert.SerializeObject(ProductId);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"/api/Cart/RemoveItemFromCart?ProductId={ProductId}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = ResponseMessages.productRemoved;
                    _logger.LogInformation("Product removed from cart successfully.");
                    return RedirectToAction("ViewCart", "Cart");
                }
                return RedirectToAction("ViewCart", "Cart");
            }
            catch (Exception)
            {
                _logger.LogError("Error occured during the process request.");
                throw;
            }
        }*/


    }
}
