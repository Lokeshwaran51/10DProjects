using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmazonClone.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Index action started.");

                var email = HttpContext.Session.GetString("Email");
                _logger.LogInformation("Retrieved email from session: {Email}", email);

                var categoryResponse = await _httpClient.GetAsync("Category/GetAllCategories");
                List<Category> categories = new();

                if (categoryResponse.IsSuccessStatusCode)
                {
                    var data = await categoryResponse.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(data);
                    _logger.LogInformation("Fetched {Count} categories from API.", categories.Count);
                }
                else
                {
                    _logger.LogWarning("Failed to fetch categories. StatusCode: {StatusCode}", categoryResponse.StatusCode);
                }

                int cartItemCount = 0;
                if (!string.IsNullOrEmpty(email))
                {
                    var countResponse = await _httpClient.GetAsync($"Cart/CartItemCount?email={email}");
                    if (countResponse.IsSuccessStatusCode)
                    {
                        var countString = await countResponse.Content.ReadAsStringAsync();
                        int.TryParse(countString, out cartItemCount);
                        _logger.LogInformation("Cart item count for {Email}: {Count}", email, cartItemCount);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to fetch cart count. StatusCode: {StatusCode}", countResponse.StatusCode);
                    }
                }

                ViewBag.CartItemCount = cartItemCount;
                ViewBag.Email = email;

                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Index action.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Home/GetSubCategoryByCategoryId/{CategoryId}")]
        public async Task<JsonResult> GetSubCategoryByCategoryId(int CategoryId)
        {
            try
            {
                _logger.LogInformation("Getting subcategories for CategoryId: {CategoryId}", CategoryId);

                var res = await _httpClient.GetAsync($"Category/GetSubCategoryByCategoryId/{CategoryId}");

                List<SubCategory> subCategories = new();
                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                    _logger.LogInformation("Fetched {Count} subcategories.", subCategories.Count);
                }
                else
                {
                    _logger.LogWarning("Failed to fetch subcategories. StatusCode: {StatusCode}", res.StatusCode);
                }

                return Json(subCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting subcategories.");
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet("SubCategories")]
        public async Task<IActionResult> SubCategories(int CategoryId)
        {
            try
            {
                _logger.LogInformation("SubCategories action started for CategoryId: {CategoryId}", CategoryId);

                var res = await _httpClient.GetAsync($"Category/GetSubCategoryByCategoryId/{CategoryId}");
                List<SubCategory> subCategories = new();

                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                    _logger.LogInformation("Fetched {Count} subcategories.", subCategories.Count);
                }
                else
                {
                    _logger.LogWarning("Failed to fetch subcategories. StatusCode: {StatusCode}", res.StatusCode);
                }

                return View(subCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SubCategories action.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
