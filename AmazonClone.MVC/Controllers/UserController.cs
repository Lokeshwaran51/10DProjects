using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
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
                    var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    var res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/User/Login", content);
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "User Logged in Successfully.";
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
