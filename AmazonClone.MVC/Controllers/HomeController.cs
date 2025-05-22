using AmazonClone.MVC.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static AmazonClone.MVC.Models.ViewModel;

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
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/CategoryControllerAPI/GetAllCategories");
           /* var res = await _httpClient.GetAsync(_httpClient.BaseAddress + "/CategoryControllerAPI/GetSubCategoryByCategoryId/CategoryId");*/
            List<Category> categories = new List<Category>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(data);
            }
            return View(categories); 
        }

        [HttpGet("Home/GetSubCategoryByCategoryId/{CategoryId}")]
        public async Task<JsonResult> GetSubCategoryByCategoryId(int CategoryId)
        {
            var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/CategoryControllerAPI/GetSubCategoryByCategoryId/{CategoryId}");

            List<SubCategory> subCategories = new List<SubCategory>();
            if (res.IsSuccessStatusCode)
            {
                var data = await res.Content.ReadAsStringAsync();
                subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
            }
            return Json(subCategories);
        }

        public async Task<IActionResult> SubCategories(int CategoryId)
        {
            var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/CategoryControllerAPI/GetSubCategoryByCategoryId/{CategoryId}");

            List<SubCategory> subCategories = new List<SubCategory>();
            if (res.IsSuccessStatusCode)
            {
                var data = await res.Content.ReadAsStringAsync();
                subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
            }
            return View(subCategories);
        }
    }
}
