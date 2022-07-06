using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
