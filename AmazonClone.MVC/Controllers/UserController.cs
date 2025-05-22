using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static AmazonClone.MVC.Models.RegisterModel;
using static AmazonClone.MVC.Models.ViewModel;
using LoginModel = AmazonClone.MVC.Models.LoginModel;
using User = AmazonClone.MVC.Models.ViewModel.User;


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
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/UserControllerAPI/Register", content);

                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Registered Successfully.";
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/UserControllerAPI/Login", content);

                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Logged in Successfully.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
