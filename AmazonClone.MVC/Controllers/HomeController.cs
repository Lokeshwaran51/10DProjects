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
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Visited Home/Index at {Time}", DateTime.Now);
                string email = HttpContext.Session.GetString("Email");
                _logger.LogInformation("User email from session: {Email}", email ?? "Not logged in");
                // Get categories
                HttpResponseMessage categoryResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Category/GetAllCategories");
                List<Category> categories = new List<Category>();
                if (categoryResponse.IsSuccessStatusCode)
                {
                    string data = await categoryResponse.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(data);
                }

                // Get cart item count if email exists
                int cartItemCount = 0;
                if (!string.IsNullOrEmpty(email))
                {
                    HttpResponseMessage countResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Cart/CartItemCount?email={email}");
                    if (countResponse.IsSuccessStatusCode)
                    {
                        string countString = await countResponse.Content.ReadAsStringAsync();
                        int.TryParse(countString, out cartItemCount);
                    }
                }

                ViewBag.CartItemCount = cartItemCount;
                ViewBag.Email = email;

                return View(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Home/GetSubCategoryByCategoryId/{CategoryId}")]
        public async Task<JsonResult> GetSubCategoryByCategoryId(int CategoryId)
        {
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category/GetSubCategoryByCategoryId/{CategoryId}");

                List<SubCategory> subCategories = new List<SubCategory>();
                if (res.IsSuccessStatusCode)
                {
                    string data = await res.Content.ReadAsStringAsync();
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                }
                return Json(subCategories);
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet("SubCategories")]
        public async Task<IActionResult> SubCategories(int CategoryId)
        {
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category/GetSubCategoryByCategoryId/{CategoryId}");
                List<SubCategory> subCategories = new List<SubCategory>();
                if (res.IsSuccessStatusCode)
                {
                    string data = await res.Content.ReadAsStringAsync();
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                }
                return View(subCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
