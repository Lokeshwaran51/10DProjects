using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.MVC.Controllers
{
    public class SampleController : Controller
    {
        public IActionResult SampleView()
        {
            return View();
        }
    }
}
