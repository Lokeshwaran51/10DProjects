using AmazonClone.MVC.Models;
using AmazonClone.MVC.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AmazonClone.MVC.Services
{
    public class UserService:IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            string? userServiceUrl = _configuration["Services:UserServiceUrl"];
            if (string.IsNullOrWhiteSpace(userServiceUrl))
            {
                throw new InvalidOperationException("UserServiceUrl is not configured in appsettings.json");
            }
            _httpClient.BaseAddress = new Uri(userServiceUrl);

        }

        public async Task<HttpResponseMessage> Register(User user,string Token)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            string data = JsonConvert.SerializeObject(user);
            StringContent content=new StringContent(data, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/User/Register", content);
        }

        public async Task<HttpResponseMessage> Login(Login login)
        {
            string data = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/User/Login", content);
        }

    }
}
