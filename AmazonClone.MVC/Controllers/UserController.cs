using AmazonClone.MVC.Constant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [HttpGet]
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
                    string token = HttpContext.Session.GetString("Token");
                    if (!string.IsNullOrEmpty(token))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await _httpClient.PostAsync(_httpClient.BaseAddress + "/User/Register", content);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = ResponseMessages.successMessageRegister;
                        return RedirectToAction("Login", "User");
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        string responseBody = await res.Content.ReadAsStringAsync();
                        dynamic response = JsonConvert.DeserializeObject(responseBody);
                        ViewBag.ErrorMessage = response.message ?? ResponseMessages.userExists;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = ResponseMessages.internalServerError;
                        return RedirectToAction("Register", "User");
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessages.internalServerError + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/User/Login", content);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", ResponseMessages.invalidCredentials);
                    return View(model);
                }
                //string token = responseContent.Trim('"');
                JObject tokenData = JObject.Parse(responseContent);
                string token = tokenData["token"].ToString();

                // Store token in session
                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("Email", model.Email);

                TempData["SuccessMessage"] = ResponseMessages.successMessageLogin;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessages.internalServerError + ex.Message);
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
            HttpResponseMessage res = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/User/Account?Email={Email}");
            if (res.IsSuccessStatusCode)
            {
                string data = await res.Content.ReadAsStringAsync();
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
