using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
