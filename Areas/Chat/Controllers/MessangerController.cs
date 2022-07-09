using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Areas.Chat.Controllers
{
    public class MessangerController : Controller
    {
        public IActionResult Messanger()
        {
            return View();
        }


    }
}
