using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Areas.Chat.Controllers
{
    public class MessangerController : Controller
    {
        [HttpGet]
        public IActionResult Messanger()
        {
            return View();
        }
        [HttpPost]
        public string Messanger(string username)
        {
            return username;
        }

    }
}
