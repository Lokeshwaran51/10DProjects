using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Login = AmazonClone.MVC.Models.Login;
using User = AmazonClone.MVC.Models.User;
namespace AmazonClone.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiUrl:BaseUrl"]);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = JsonConvert.SerializeObject(model);
                    var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/User/Register", content);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "User Registered Successfully.";
                        return RedirectToAction("Login", "User");
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        var responseBody = await res.Content.ReadAsStringAsync();
                        dynamic response = JsonConvert.DeserializeObject(responseBody);
                        ViewBag.ErrorMessage = response.message ?? "User already exists. Please login to continue.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "An error occurred while registering. Please try again.";
                        return RedirectToAction("Register", "User");
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = JsonConvert.SerializeObject(model);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/User/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                        HttpContext.Session.SetString("Email", loginResponse.Email);
                        HttpContext.Session.SetString("Token", loginResponse.Token);

                        TempData["SuccessMessage"] = "User Logged in Successfully.";
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid login credentials.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }


        [HttpGet]
        public async Task<IActionResult> Account(string Email)
        {
            var res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/User/Account?Email={Email}");
            if (res.IsSuccessStatusCode)
            {
                var data = await res.Content.ReadAsStringAsync();
                //subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(data);
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
