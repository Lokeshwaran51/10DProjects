using AmazonClone.MVC.Models;
using AmazonClone.MVC.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AmazonClone.MVC.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CartService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            string? cartServiceUrl = _configuration["Services:CartServiceUrl"];
            if (string.IsNullOrEmpty(cartServiceUrl))
                throw new ArgumentNullException(nameof(cartServiceUrl), "CartServiceUrl is missing in configuration");

            _httpClient.BaseAddress = new Uri(cartServiceUrl);
        }

        public async Task<HttpResponseMessage> AddToCart(AddToCartDTO addToCartDTO,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(addToCartDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiUrl = _httpClient.BaseAddress + "/api/cart/AddToCart";

            return await _httpClient.PostAsync(apiUrl, content);
        }

        public async Task<HttpResponseMessage> CartItemCount(string email, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync($"api/Cart/CartItemCount?email={email}");

            /* var apiUrl = $"{_httpClient.BaseAddress}api/Cart/CartItemCount?email={email}";
             return await _httpClient.GetAsync(apiUrl);*/
        }


        public async Task<HttpResponseMessage> ViewCart(string Email, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetAsync($"api/Cart/GetCartItems?email={Email}");
        }

        public async Task<HttpResponseMessage> RemoveItemFromCart(int ProductId, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var json = JsonConvert.SerializeObject(ProductId);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/Cart/RemoveItemFromCart", content);
        }
    }
}
