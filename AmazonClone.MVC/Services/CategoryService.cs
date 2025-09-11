using AmazonClone.MVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace AmazonClone.MVC.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CategoryService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            string? categoryServiceUrl = _configuration["Services:CategoryServiceUrl"];
            _httpClient.BaseAddress = new Uri(categoryServiceUrl);
        }

        public async Task<HttpResponseMessage> GetAllCategories(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync("api/Category/GetAllCategories");
        }

        public async Task<HttpResponseMessage> GetSubCategoryByCategoryId(int categoryId,string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync("api/Category/GetSubCategoryByCategoryId");
        }
    }
}
