using AmazonClone.MVC.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static AmazonClone.MVC.Models.Category;

namespace AmazonClone.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var email = HttpContext.Session.GetString("Email");
                // Get categories
                var categoryResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Category/GetAllCategories");
                List<Category> categories = new List<Category>();
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var data = await categoryResponse.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(data);
                }

                // Get cart item count if email exists
                int cartItemCount = 0;
                if (!string.IsNullOrEmpty(email))
                {
                    var countResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Cart/CartItemCount?email={email}");
                    if (countResponse.IsSuccessStatusCode)
                    {
                        var countString = await countResponse.Content.ReadAsStringAsync();
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
                var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category/GetSubCategoryByCategoryId/{CategoryId}");

                List<SubCategory> subCategories = new List<SubCategory>();
                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                }
                return Json(subCategories);
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        public async Task<IActionResult> SubCategories(int CategoryId)
        {
            try
            {
                var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category/GetSubCategoryByCategoryId/{CategoryId}");
                List<SubCategory> subCategories = new List<SubCategory>();
                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadAsStringAsync();
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
