using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
