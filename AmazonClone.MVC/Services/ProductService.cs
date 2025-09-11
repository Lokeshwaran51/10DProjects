using NuGet.Common;
using System.Net.Http.Headers;

namespace AmazonClone.MVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration,HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

            string? productServiceUrl = _configuration["Services:ProductServiceUrl"];
            _httpClient.BaseAddress = new Uri(productServiceUrl);
        }

        public async Task<HttpResponseMessage> GetListOfProductsBySubCategoryId(int SubCategoryId, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync($"api/Product/GetListOfProductsBySubCategoryId/{SubCategoryId}");
        }

        public async Task<HttpResponseMessage> ProductDetails(int Id, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync($"api/Product/ProductDetails/{Id}");
        }
    }
}
